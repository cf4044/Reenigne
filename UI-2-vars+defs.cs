using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Reenigne
{
#pragma warning disable IDE1006
    enum boardMouseModes
    {   // Determines what is done when mouse is clicked on the board image
        none,
		addComponent,		// place a component on the board
		drawTrace,          // draw copper trace
        insertVia,          // insert a via 
        insertThroughHole,  // insert a through hole
        selectItem          // select a trace, via, through hole or component
    };

	enum circuitMouseModes
	{
		mainMode,               // The defaut mode - selection of nets and components
		dragSelecting,          // In the process of dragging a frame to select multiple items
		draggingSelection,		// Dragging the items within the selection box
		dragComponent,			// dragging a component, or section thereof
		dragCentroid,
		drawConnectionLine,
		editConnectionLine,
		
	}

    [Flags]
    enum boardSelectionModes
    {   // Determines what is selected when mouse is clicked on the board image
        none		= 0,
        point		= 1,
        line		= 2,
        segment		= 4,
        net			= 8,
        via			= 16,
		component	= 32
    }

	public enum mirrorModes { none, horizontal, vertical }            // mirror mode of each layer. Typically layer 0 will be 'none' and layer 1 will be 'horizontal'

	public class circuitSelections
	{
		public class componentSection
		{
			public PointF initialPosition, finalPosition;
			public int componentNum, sectionNum;
		};
		public class connectionPoint
		{
			public PointF initialPosition, finalPosition;
			public int netNum, connectionLineNumber, pointNumber;
		};
		public class centroid
		{
			public PointF initialPosition, finalPosition;
			public int netNum;
		};

		public float initialX, initialY;
		public RectangleF selectionBox = new RectangleF();
		public List<componentSection> componentSectionList	= new List<componentSection>();
		public List<connectionPoint>  connectionPointList	= new List<connectionPoint>();
		public List<centroid>         centroidList          = new List<centroid>();
	}


    public partial class mainForm : Form
    {
		// Definitions


		// Generic UI action related
		int     mouseWheelStartX                = 0;
        int     mouseWheelStartY                = 0;
        bool    mouseWheelPressed               = false;
		float	mouseLeftStartX					= 0;
		float	mouseLeftStartY					= 0;
		bool    mouseLeftPressed                = false;
		float   mouseRightStartX                = 0;
		float   mouseRightStartY                = 0;


		// Displaying and handling the board 
		public List<boardImage>		boardImages						= new List<boardImage>(); 
        public static traceHandler	traceHandler					= new traceHandler();
		public static int			layerZZ							= 0;
		public static int			layer
		{
			get
			{
				int numLayers						= traceHandler.layers.Count();
				if( layerZZ < 0 ) layerZZ			= 0;
				if( layerZZ >= numLayers ) layerZZ	= numLayers - 1;
				return layerZZ;
			}
			set
			{
				int numLayers = traceHandler.layers.Count();
				if( value >= 0 && value < numLayers )	layerZZ = value;   
				if( layerZZ <  0		 )				layerZZ = 0;
				if( layerZZ >= numLayers )				layerZZ = numLayers - 1;
				for( int i = 0; i < glob.mainForm.boardImages.Count(); i++ )
				{
					glob.mainForm.boardImages[i].shown = ( glob.mainForm.boardImages[i].layer == layer );
				}
				layerInfo lyr = traceHandler.layers.Find( r => r.layer == layer );
				if( traceHandler.layers.Count() > 0 )
					if( lyr != null )
						glob.mainForm.textBoxLayerName.Text = lyr.layer.ToString() + ":" + lyr.name;
					else
						glob.mainForm.textBoxLayerName.Text = "No layer definition";
				glob.mainForm.redrawBoards();
				glob.mainForm.Refresh();
			}
		}
		boardMouseModes				boardMouseMode					= boardMouseModes.selectItem;
		bool						boardPictureBoxActive           = false;
		Bitmap						boardBitMap;
		public Graphics				boardGraphics;
        List<PointF>				tracePoints						= new List<PointF>();				// this holds the points of a new trace as it is being drawn
        public Color[]				traceColour						= { Color.Yellow, Color.Violet, Color.Blue, Color.Beige, Color.Bisque };       //KLUDGE the colour of the trace on each layer
		formLayerConfig             lCForm;																// Form for the layers and images selection				

		// Schematic diagram handling
		circuitMouseModes			circuitMouseMode				= circuitMouseModes.mainMode;
		circuitMouseModes			circuitMouseModePrevious		= circuitMouseModes.mainMode;
		int[]						selectedConnectionPointIndex	= new int[3];				// Indices identifying point in connection line that is being dragged
		List<RectangleF>			selectionBoxes					= new List<RectangleF>();   // List of selected areas
		List<circuitSelections>		circuitSelections				= new List<circuitSelections>();
		bool						circuitPictureBoxActive			= false;
		int							circuitZoomIndex				= 0;
		RectangleF					circuitSelectionFrame			= new Rectangle();
		public static schematic		schem							= new schematic();
		List<PointF>				connectionLine					= new List<PointF>();	// temporary list of points for drawing connections between net centroid and component pin
		Bitmap						schemBitMap;
		public static Graphics		schemGraphics;
		public float				schemGridSnap					= 100f;


		// parameters for the UI window controls
		int leftPanelWidth      = 100;
        int midPanelWidth       = 100;
        int rightPanelWidth     = 100;
        int topPanelHeight      = 100;
        int bottomPanelHeight   = 100;
        float panelSplitRatio   = 0.5f;


        // Selection of items common to both Board and Schematic
        public static int netSelector				=  0;
        public static int netSelectorIndex			= -1;
		public static int componentSelectorIndexZZ	= -1;
		public static int componentSelectorIndex
		{
			get { return componentSelectorIndexZZ; }
			set
			{
				componentSelectorIndexZZ = value;
				glob.mainForm.updateComponentInfoBox();	
			}
		}


		// Selection items for Board display
		public static int segmentSelectorIndex		= -1;
        public static int viaSelectorIndex			= -1;
		
		// Selection items for Schematic display
		public static int sectionSelectorIndexZZ	= -1;
		public static int sectionSelectorIndex
		{
			get { return sectionSelectorIndexZZ; }
			set
			{
				sectionSelectorIndexZZ = value;
				glob.mainForm.updateComponentInfoBox();
			}
		}
		public static int centroidSelectorIndex		= -1;

        // other stuff
        

    }
}