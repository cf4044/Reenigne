using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reenigne
{
#pragma warning disable IDE1006

    public static class glob
    {
		public static mainForm mainForm;
		
		// Various configuration variables
		public static string applicationName        = "Reenigne";
		public static string projectFolder			= "D:\\Dropbox\\o\\Reenigne\\TestProject";
		public static string projectName			= "newProject";
		public static string projectFileSuffix		= ".rgn";
		public static string symbolLibraryFolder	= "D:\\Dropbox\\o\\Reenigne\\TestProject\\Symbol library";
		public static int selectionRadius = 10;		// the selection patch size when selecting items on the board view with the mouse 

		// stuff
		//public static int qx;
		public static long debugCount;				// counter for line numbering of debug printouts
		public static IProgress<string>				progress;

		// geometric calculations
		public static PointF	addPointF( PointF p1, PointF p2 )
		{
			PointF P = new PointF( p1.X + p2.X, p1.Y + p2.Y );
			return P;
		}
		public static float		distanceBetweenPoints( PointF pt1, PointF pt2 )
		{
			return (float)Math.Sqrt( ( pt2.X - pt1.X ) * ( pt2.X - pt1.X ) + ( pt2.Y - pt1.Y ) * ( pt2.Y - pt1.Y ) );
		}
		public static float		distancePointFromLine( PointF point, PointF lineStart, PointF lineEnd )
		{
			float xPoint = point.X;
			float yPoint = point.Y;

			float xStart = lineStart.X;
			float yStart = lineStart.Y;

			float xEnd = lineEnd.X;
			float yEnd = lineEnd.Y;

			float a = lineEnd.X - lineStart.X;
			float b = lineEnd.Y - lineStart.Y;
			float c = lineStart.X;
			float d = lineStart.Y;

			if( point == lineStart || point == lineEnd ) return 0f;

			if( lineStart.Equals( lineEnd ) ) return distanceBetweenPoints( point, lineEnd );
			if( xStart == xEnd )
			{
				if( yPoint < Math.Max( yStart, yEnd ) && yPoint > Math.Min( yStart, yEnd ) ) return ( Math.Abs( xPoint - xStart ) );
				else return Math.Min( distanceBetweenPoints( point, lineStart ), distanceBetweenPoints( point, lineEnd ) );
			}
			if( yStart == yEnd )
			{
				if( xPoint < Math.Max( xStart, xEnd ) && xPoint > Math.Min( xStart, xEnd ) ) return ( Math.Abs( yPoint - yStart ) );
				else return Math.Min( distanceBetweenPoints( point, lineStart ), distanceBetweenPoints( point, lineEnd ) );
			}

			float xIntersection = a * (yPoint - d) / b + c;
			float yIntersection = b * (xPoint - c) / a + d;

			float dthi                  = xIntersection - xPoint;    // distanceToHorizontalIntersection
			float dtvi                  = yIntersection - yPoint;    // distanceToVerticalIntersection
			float dfptl                 = (float)Math.Sqrt( ( dthi * dthi + dtvi * dtvi) / (( dtvi / dthi + dthi / dtvi ) * ( dtvi / dthi + dthi / dtvi ) ) );    // distance from point to line
			float hypotenuse            = (float)Math.Sqrt( dthi * dthi + dtvi * dtvi);
			float sinAlpha              = ( dthi / hypotenuse ) * Math.Sign(dthi * dtvi);
			float cosAlpha              = ( dtvi / hypotenuse) * Math.Sign(dthi * dtvi);
			float xNormalIntersection   = xPoint + dfptl * cosAlpha;
			float yNormalIntersection   = yPoint + dfptl * sinAlpha;
			float parameter             = (xNormalIntersection - c) / a;
			float limitedParameter      = Math.Min( 1f, Math.Max( 0f, parameter ) );
			float xNearest              = a * limitedParameter + c;
			float yNearest              = b * limitedParameter + d;
			float distance              = (float)Math.Sqrt( (xNearest - xPoint) * (xNearest - xPoint) + (yNearest - yPoint) * (yNearest - yPoint));

			return distance;
		}
		public static PointF	lineIntersection( PointF line0Start, PointF line0End, PointF line1Start, PointF line1End )
		{
			float distance = float.NaN;
			return lineIntersection( line0Start, line0End, line1Start, line1End, out distance );
		}
		public static PointF	lineIntersection( PointF line0Start, PointF line0End, PointF line1Start, PointF line1End, out float distance )
		{
			float[] x1 = { line0Start.X, line1Start.X };
			float[] y1 = { line0Start.Y, line1Start.Y };
			float[] x2 = { line0End.X  , line1End.X   };
			float[] y2 = { line0End.Y  , line1End.Y   };

			float[] mx = new float[2];
			float[] my = new float[2];
			float[] cx = new float[2];
			float[] cy = new float[2];
			float[] t  = new float[2];

			float x, y;

			for( int i = 0; i < 2; i++ )
			{
				mx[i] = x2[i] - x1[i];
				cx[i] = x1[i];
				my[i] = y2[i] - y1[i];
				cy[i] = y1[i];
			}
			t[0] = -1;
			t[1] = -1;
			distance = float.NaN;
			if( ( mx[0] * my[1] - my[0] * mx[1] ) != 0 && ( mx[1] * my[0] - my[1] * mx[0] ) != 0 )
			{
				for( int i = 0; i < 2; i++ )
				{
					t[i] = mx[1 - i] * ( cy[i] - cy[1 - i] ) - my[1 - i] * ( cx[i] - cx[1 - i] );
					t[i] = t[i] / ( mx[i] * my[1 - i] - my[i] * mx[1 - i] );
				}
			}

			if( t[0] >= 0 && t[0] <= 1 && t[1] >= 0 && t[1] <= 1 )
			{   // The lines intersect so we return the co-ordinates of where they intersect
				x = mx[0] * t[0] + cx[0];
				y = my[0] * t[0] + cy[0];
				distance = 0;
			}
			else
			{   // no intersection so we find the distance between the nearest parts of the two lines
				distance = ( distancePointFromLine( line0Start, line1Start, line1End ) );
				distance = Math.Min( distance, distancePointFromLine( line0End, line1Start, line1End ) );
				distance = Math.Min( distance, distancePointFromLine( line1Start, line0Start, line0End ) );
				distance = Math.Min( distance, distancePointFromLine( line1End, line0Start, line0End ) );
				x = float.NaN;
				y = float.NaN;
			}

			return new PointF( x, y );
		}

		// file read functions
		public static string	readString	( string tag, string value, string fieldName )
		{
			if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
			return value;
		}
		public static char		readChar	( string tag, string value, string fieldName )
		{
			if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
			return char.Parse( value );
		}
		public static int		readInt		( string tag, string value, string fieldName )
		{
			if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
			return int.Parse( value );
		}
		public static float		readFloat	( string tag, string value, string fieldName )
		{
			if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
			return float.Parse( value );
		}
		public static bool		readBool	( string tag, string value, string fieldName )
		{
			if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
			if( value != "True" && value != "False" ) causeException( "Value error. Wanted " + fieldName + ", got " + value ); ;
			return ( value == "True" );
		}
		public static PointF	readPointF	( string tag, string valueX, string valueY, string fieldName )
		{
			if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag );
			float x,y;
			char[] charsToTrim = { '{', 'X', '=', ' ', 'Y', '}' };
			x = float.Parse( valueX.Trim( charsToTrim ) );
			y = float.Parse( valueY.Trim( charsToTrim ) );
			return new PointF( x, y );
		}

		// other functions
		public static void		causeException() { causeException( "Not specified" ); }
		public static void		causeException( string errorMessage ) //KLUDGE development only
		{
			Console.WriteLine( "Exception thrown: {0}", errorMessage );
			object o2 = null;
			//int i2 = (int)o2;   // Error
		}
		public static int		GetEnumFromDescription( string description, Type enumType )
		{
			foreach( var field in enumType.GetFields() )
			{
				if( field.Name == description )
					return (int)field.GetValue( null );
			}
			return 0;
		}
		public static string	nthCharacter( string s, int index )
		{
			if( index < s.Length ) return s.Substring( index, 1 );
			return "";
		}
		public static string	cleanText( string s )
		{   // removes commas from text strings so they are not mistaken for delimiters
			string ss =  s.Replace( ",", "_" );
			return ss;
		}

	}




	static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			// the appliction runs over and over again until the form exits with the value of 'Quit' set to true
			do			 
			{
				glob.mainForm = new mainForm();
				Application.Run( glob.mainForm );
			}
			while( !mainForm.quit );

			
			DoNothing();
        }
        
        static void DoNothing()
        {
        }
    }
}
