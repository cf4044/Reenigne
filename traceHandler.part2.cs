using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reenigne
{   // Building, displaying and handling of the traces. 
#pragma warning disable IDE1006


	public enum viaTypes { via, thruHole };

	public class segment
	{
		public int layer;
		public int net;
		public int colour;
		public float thickness;

		public List<PointF> pointList = new List<PointF>();

		public segment() { }
		public segment( int layer, int net, int colour, float thickness, List<PointF> pointList )
		{
			this.layer = layer;
			this.net = net;
			this.colour = colour;
			this.thickness = thickness;
			this.pointList = pointList;
		}

		public int Net { get { return net; } set { net = value; } }
		public List<PointF> PointList { get { return pointList; } set { pointList = value; } }
	}

	public class via
	{
		public int net;
		public float size;
		public viaTypes viaType;
		public PointF location;

		public via( int net, float size, PointF location )
		{
			this.net = net;
			this.size = size;
			this.location = location;
		}

		//public int Net { get; set; }
		//public float Size { get; set; }
		//public viaTypes ViaType { get; set; }
		//public PointF Location { get; set; }
	}

	public class layerInfo
	{
		public int		layer;
		public string	name;
		public bool		mirrored;		// set true for back layer for correct orientation of components
	}

	public partial class traceHandler  //public partial class mainForm : Form
	{
		public List<layerInfo>	layers = new List<layerInfo>();
		public List<segment>	segmentList = new List<segment>();
		public List<int>		netList = new List<int>();
		public List<via>		viaList = new List<via>();
		public List<component>	componentss    = new List<component>();     // double s because Windows Forms is already using the name 'components'
		public bool netRetraceNeeded = false;
	}
}