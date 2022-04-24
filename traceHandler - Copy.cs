﻿using System;
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
{   // Building, displaying and handling of the traces. One instance is needed.
#pragma warning disable IDE1006


    enum viaTypes { via, thruHole };

    class segment
    {
        public int layer;
        public int net;
        public int colour;
        public float thickness;
        public List<PointF> pointList = new List<PointF>();

        public segment() { }
        public segment(int layer, int net, int colour, float thickness, List<PointF> pointList)
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

    
    class via
    {
        public int net;
        public float size;
        public viaTypes viaType;
        public PointF location;

        public via(int net, float size, PointF location)
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


class traceHandler
    {
        public List<segment>    segmentList = new List<segment>();
        public List<int>        netList     = new List<int>();
        public List<via> viaList = new List<via>();

        public traceHandler()
        {
        }

        // Editing traces
        public void addTrace(List<PointF> pointList, int layer, int net)
        {
            List<PointF> clonedList = new List<PointF>(pointList);
            segmentList.Add
            (
                new segment(layer, net, 155, 5, clonedList)   // not sure what happened here - need to fix
                {
                    layer = layer,
                    net = net,
                    colour = 155,
                    thickness = 5,
                    pointList = clonedList
                }
            );

            //Console.WriteLine("   ");
            //for (int i = 0; i < segmentList.Count(); i++)
            //{
            //    Console.WriteLine("i={0} count={1}", i, segmentList[i].pointList.Count());
            //}
        }
        public void deleteSegment( int segmentSelectorIndex )
        {
            Console.WriteLine("DELETE SEGMENT - not ready yet!!");
        }
        public void deleteNet( int netSelector )
        {
            if (netSelector > 0 && segmentList.Count() > 0 )
            {
                for (int i = segmentList.Count() - 1; i >= 0; i--)
                {
                    if (segmentList[i].net == netSelector)
                    {
                        segmentList.RemoveAt(i);
                    }
                }
            }
        }
        public void AddVia(via via)
        {
            viaList.Add(via);
        }
        public void AddVia(PointF position, float viaSize)
        {
            Console.Write("Adding via. Count was {0}", viaList.Count());
            viaList.Add(new via(net : 0, size : viaSize, location : position));
            Console.WriteLine(" and is now {0}", viaList.Count());
        }
        public void deleteVia( int viaIndex)
        {
            if ( viaIndex>=0 && viaIndex<viaList.Count() )
            {
                viaList.RemoveAt(viaIndex);
            }
        }

        // File load and save
        public void writeSegments(StreamWriter writer)
        {
            int numSegments = segmentList.Count();
            int numPoints;
            writer.WriteLine("startSegments");
            writer.WriteLine("numSegments,{0}", numSegments);
            for (int i = 0; i < numSegments; i++)
            {
                writer.Write("layer,{0},", segmentList[i].layer);
                writer.Write("net,{0},", segmentList[i].net);
                writer.Write("colour,{0},", segmentList[i].colour);
                writer.Write("thickness,{0},", segmentList[i].thickness);
                writer.WriteLine("numPoints,{0}", numPoints = segmentList[i].pointList.Count());
                for (int j = 0; j < numPoints; j++)
                {
                    if (j != 0) writer.Write(",");
                    PointF p = segmentList[i].pointList[j];
                    writer.Write("{0},{1}", p.X, p.Y);
                }
                writer.WriteLine("");
            }
            writer.WriteLine("endSegments");
        }
        public void readSegments(StreamReader reader)
        {
            int numSegments = 0;
            int numPoints;
            segment seg = new segment(0, 0, 0, 0, new List<PointF>());
            string a;
            string[] b;
            int p;

            Console.WriteLine(">>>Starting");
            //try
            {

                // Get number of segments
                a = reader.ReadLine();
                a = reader.ReadLine();
                b = a.Split(',');
                if (!b[0].Equals("numSegments")) causeException();
                numSegments = int.Parse(b[1]);
                Console.WriteLine("Reading {0} segments", numSegments);

                //seg.pointList = new List<PointF>();
                for (int i = 0; i < numSegments; i++)
                {
                    // Get layer, colour, thickness and numPoints
                    a = reader.ReadLine();
                    b = a.Split(',');
                    p = 0;
                    if (!b[p++].Equals("layer")) causeException("Read mismatch " + i.ToString() + ", " + p.ToString());
                    seg.layer = int.Parse(b[p++]);
                    if (!b[p++].Equals("net")) causeException("Read mismatch " + i.ToString() + ", " + p.ToString());
                    seg.net = int.Parse(b[p++]);
                    if (!b[p++].Equals("colour")) causeException("Read mismatch " + i.ToString() + ", " + p.ToString());
                    seg.colour = int.Parse(b[p++]);
                    if (!b[p++].Equals("thickness")) causeException("Read mismatch " + i.ToString() + ", " + p.ToString());
                    seg.thickness = int.Parse(b[p++]);
                    if (!b[p++].Equals("numPoints")) causeException("Read mismatch " + i.ToString() + ", " + p.ToString());
                    numPoints = int.Parse(b[p++]);

                    // Read the list of points.
                    a = reader.ReadLine();
                    b = a.Split(',');
                    seg.pointList.Clear();
                    for (int j = 0; j < numPoints; j++)
                    {
                        seg.pointList.Add(new PointF { X = float.Parse(b[j * 2]), Y = float.Parse(b[j * 2 + 1]) });
                    }
                    addTrace(seg.pointList, seg.layer, seg.net);
                }
                //while (reader.Peek() != -1);
                a = reader.ReadLine();

            }
            //catch
            //{
            //    Console.WriteLine(">>>Something went WRONG !!");
            //}
            //finally
            //{
            //    Console.WriteLine(">>>Ready");
            //}

        }
        public void writeVias(StreamWriter writer)
        {
            int numVias = viaList.Count();
            writer.WriteLine("startVias");
            writer.WriteLine("numVias,{0}", numVias);
            for (int i = 0; i < numVias; i++)
            {
                //writer.Write("layer,{0},", viaList[i].layer);
                writer.Write("net,{0},", viaList[i].net);
                writer.Write("size,{0},", viaList[i].size);
                writer.Write("viaType,{0},", viaList[i].viaType);
                writer.Write("X,{0},", viaList[i].location.X);
                writer.WriteLine("Y,{0}", viaList[i].location.Y);
            }
            writer.WriteLine("endVias");
        }
        public void readVias(StreamReader reader)
        {
            int numVias = 0;
            string a;
            string[] b;
            int p;
            //try
            {

                // Get number of vias
                a = reader.ReadLine();
                a = reader.ReadLine();
                b = a.Split(',');
                if (!b[0].Equals("numVias")) causeException();
                numVias = int.Parse(b[1]);
                Console.WriteLine("Reading {0} vias", numVias);
                for (int i = 0; i < numVias; i++)
                {
                    via via = new via(0, 0, new PointF(0, 0));
                    // Get layer, colour, thickness and numPoints
                    a = reader.ReadLine();
                    b = a.Split(',');
                    p = 0;
                    if (!b[p++].Equals("net")) causeException("Read mismatch " + i.ToString() + ", " + p.ToString());
                    via.net = int.Parse(b[p++]);
                    if (!b[p++].Equals("size")) causeException("Read mismatch " + i.ToString() + ", " + p.ToString());
                    via.size = int.Parse(b[p++]);
                    if (!b[p++].Equals("viaType")) causeException("Read mismatch " + i.ToString() + ", " + p.ToString());
                    via.viaType = (viaTypes)GetEnumFromDescription(b[p++],typeof (viaTypes));     //(viaTypes)int.Parse(b[p++]);
                    if (!b[p++].Equals("X")) causeException("Read mismatch " + i.ToString() + ", " + p.ToString());
                    via.location.X = float.Parse(b[p++]);
                    if (!b[p++].Equals("Y")) causeException("Read mismatch " + i.ToString() + ", " + p.ToString());
                    via.location.Y = float.Parse(b[p++]);
                    AddVia(via);
                }
            }
            //catch
            //{
            //    Console.WriteLine(">>>Something went WRONG !!");
            //}
            //finally
            //{
            //    Console.WriteLine(">>>Ready");
            //}

        }


        public void findNetTopology()
        {   //find which segments are connected to eachother
            int i, j, ii, jj, iii; //, jjj;
            int thisNet, nextNet;
            PointF thisPoint;
            int thisLayer;

            // KLUDGE - dev only
            // reset all net references to zero
            for (i = 0; i < segmentList.Count(); i++) segmentList[i].net = 0;
            nextNet = 1;
            // endKludge


            // Find out which segments are connected and give each connected group a net number
            bool matched;
            // Iterate the 'source' segments
            for (i = 0; i < segmentList.Count(); i++)      
            {
                thisNet = segmentList[i].net;
                thisLayer = segmentList[i].layer;
                if (thisNet == 0)   // Give it the next net number if it doesn't already have one
                {
                    thisNet = nextNet++;
                    segmentList[i].net = thisNet;
                }
                // iterate for the points in the source segment
                for (j = 0; j < segmentList[i].pointList.Count() - 1; j++) 
                {
                    thisPoint = segmentList[i].pointList[j];        // the point to which all others will be compared
                    matched = false;
                    // iterate through the 'destination' segments
                    for (ii = i + 1; ii < segmentList.Count(); ii++)   
                    {
                        // iterate through the points in the 'destination' segment
                        for (jj = 0; jj < segmentList[ii].pointList.Count(); jj++)           
                        {
                            // Check if the point at the end of the line coincides with the end point of any other line
                            if (distanceBetweenPoints(thisPoint, segmentList[ii].pointList[jj]) < glob.selectionRadius)
                            {
                                if (thisLayer == segmentList[ii].layer)
                                {
                                    matched = true;
                                }
                                else
                                {
                                    if (distanceBetweenPoints(thisPoint, findViaAt(thisPoint).location) < glob.selectionRadius)
                                    {
                                        matched = true;
                                    }
                                }
                            }
                            // check if this line intersects another one
                            if ( !matched && j>0 && jj>0 )
                            {
                                PointF line0Start = segmentList[i].pointList[j-1];
                                PointF line0End   = segmentList[i].pointList[j];
                                PointF line1Start = segmentList[ii].pointList[jj-1];
                                PointF line1End   = segmentList[ii].pointList[jj];

                                PointF intersection = lineIntersection(line0Start, line0End, line1Start, line1End);

                                if ( !float.IsNaN ( intersection.X ) )
                                {
                                    if ( i==363 & ii==364)
                                    { }
                                    if (thisLayer == segmentList[ii].layer) matched = true;
                                    else
                                    {
                                        if (distanceBetweenPoints(intersection, findViaAt(thisPoint).location) < glob.selectionRadius)
                                        {
                                            matched = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (matched)
                        {
                            matched = false;
                            if (segmentList[ii].net == 0) // if target is not yet assigned a net then just make it equal to thisNet
                            {
                                segmentList[ii].net = thisNet;
                            }
                            if (segmentList[ii].net != thisNet)  // if target is already assigned a net then assign the new net to it and all the ones with that net 
                            {
                                int foundNet = segmentList[ii].net;
                                for (iii = 0; iii < segmentList.Count() - 1; iii++)
                                {
                                    if (segmentList[iii].net == foundNet)
                                    {
                                        segmentList[iii].net = thisNet;
                                        Console.WriteLine("Segment:{0}/net{1} assimilating Segment:{2} from net{3}", i, thisNet, iii, foundNet);
                                    }

                                }
                            }
                        }
                    }
                }
            }

            // Create a net list
            netList.Clear();
            for (i = 0; i < segmentList.Count(); i++)
            {
                thisNet = segmentList[i].net;
                if (!netList.Contains(thisNet))
                {
                    netList.Add(thisNet);
                    //Console.WriteLine("Added net {0}", thisNet);
                };
            }


        }
        public void findNetTopologyOLD()
        {   //find which segments are connected to eachother
            int i, j, ii, jj, iii; //, jjj;
            int thisNet, nextNet;
            PointF thisPoint;
            int thisLayer;

            // KLUDGE - dev only
            // reset all net references to zero
            for (i = 0; i < segmentList.Count(); i++) segmentList[i].net = 0;
            nextNet = 1;
            // endKludge


            // Find out which segments are connected and give each connected group a net number
            bool matched;
            for (i = 0; i < segmentList.Count(); i++)      // Iterate the 'source' segments
            {
                // http://csharphelper.com/blog/2014/08/determine-where-two-lines-intersect-in-c/
                thisNet = segmentList[i].net;
                thisLayer = segmentList[i].layer;
                if (thisNet == 0)   // Give it the next net number if it doesn;t already have one
                {
                    thisNet = nextNet++;
                    segmentList[i].net = thisNet;
                }
                for (j = 0; j < segmentList[i].pointList.Count() - 1; j++) // iterate for the points in the source segment
                {
                    thisPoint = segmentList[i].pointList[j];        // the point to which all others will be compared
                    matched = false;
                    for (ii = i + 1; ii < segmentList.Count(); ii++)   // iterate through the 'destination' segments
                    {
                        for (jj = 0; jj < segmentList[ii].pointList.Count(); jj++)           // iterate through the points iun the 'destination' segment
                        {
                            if (distanceBetweenPoints(thisPoint, segmentList[ii].pointList[jj]) < 10)
                            {
                                if (thisLayer == segmentList[ii].layer)
                                {
                                    matched = true;
                                }
                                else
                                {
                                    if (distanceBetweenPoints(thisPoint, findViaAt(thisPoint).location) < 10f)
                                    {
                                        matched = true;
                                    }
                                }
                            }
                        }
                        if (matched)
                        {
                            matched = false;
                            if (segmentList[ii].net == 0) // if target is not yet assigned a net then just make it equal to thisNet
                            {
                                segmentList[ii].net = thisNet;
                            }
                            if (segmentList[ii].net != thisNet)  // if target is already assigned a net then assign the new net to it and all the ones with that net 
                            {
                                int foundNet = segmentList[ii].net;
                                for (iii = 0; iii < segmentList.Count() - 1; iii++)
                                {
                                    if (segmentList[iii].net == foundNet)
                                    {
                                        segmentList[iii].net = thisNet;
                                        Console.WriteLine("Segment:{0}/net{1} assimilating Segment:{2} from net{3}", i, thisNet, iii, foundNet);
                                    }

                                }
                            }
                        }
                    }
                }
            }

            // Create a net list
            netList.Clear();
            for (i = 0; i < segmentList.Count(); i++)
            {
                thisNet = segmentList[i].net;
                if (!netList.Contains(thisNet))
                {
                    netList.Add(thisNet);
                    //Console.WriteLine("Added net {0}", thisNet);
                };
            }


        }

        public float distanceBetweenPoints( PointF pt1, PointF pt2)
        {
            return (float)Math.Sqrt( ( pt2.X - pt1.X ) * (pt2.X - pt1.X) + (pt2.Y - pt1.Y) * (pt2.Y - pt1.Y) );
        }
        public float distancePointFromLine(PointF point, PointF lineStart, PointF lineEnd)
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

            if ( lineStart.Equals(lineEnd)) return distanceBetweenPoints( point, lineEnd);
            if (xStart == xEnd)
            {
                if (yPoint < Math.Max(yStart, yEnd) && yPoint > Math.Min(yStart, yEnd)) return (Math.Abs(xPoint - xStart));
                else return Math.Min( distanceBetweenPoints(point, lineStart ), distanceBetweenPoints( point, lineEnd));
            }
            if (yStart == yEnd)
            {
                if (xPoint < Math.Max(xStart, xEnd) && xPoint > Math.Min(xStart, xEnd)) return (Math.Abs(yPoint - yStart));
                else return Math.Min(distanceBetweenPoints(point, lineStart), distanceBetweenPoints(point, lineEnd));
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
        public PointF lineIntersection(PointF line0Start, PointF line0End, PointF line1Start, PointF line1End)
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

            for ( int i=0; i<2; i++)
            {
                mx[i] = x2[i] - x1[i];
                cx[i] = x1[i];
                my[i] = y2[i] - y1[i];
                cy[i] = y1[i];
            }
            t[0] = -1;
            t[1] = -1;
            x = float.NaN ;
            y = float.NaN ;
            if ( (mx[0] * my[1] - my[0] * mx[1]) != 0  && ( mx[1] * my[0] - my[1] * mx[0] ) != 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    t[i] = mx[1 - i] * (cy[i] - cy[1 - i]) - my[1 - i] * (cx[i] - cx[1 - i]);
                    t[i] = t[i] / (mx[i] * my[1 - i] - my[i] * mx[1 - i]);
                }
            }

            if (t[0] >= 0 && t[0] <= 1 && t[1] >= 0 && t[1] <= 1) 
            {
                x = mx[0] * t[0] + cx[0];
                y = my[0] * t[0] + cy[0];
            }

            return new PointF( x, y );
        }

        public int findNetIndexAt( PointF searchPoint, int layer)
        {
            bool matched = false;
            int thisLayer, netSelector;
            int ii, jj;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (ii = 0; ii < segmentList.Count()  ; ii++)
            {
                for (jj = 0; jj < segmentList[ii].pointList.Count() - 1; jj++)
                //for (jj = 0; jj < segmentList[ii].pointList.Count(); jj++)
                    {
                        PointF thisPoint = segmentList[ii].pointList[jj];
                    thisLayer = segmentList[ii].layer;
                    if (thisLayer == layer)
                    {
                        if (distancePointFromLine ( searchPoint, segmentList[ii].pointList[jj], segmentList[ii].pointList[jj+1] ) < glob.selectionRadius)
                        //if (distanceBetweenPoints( thisPoint, searchPoint) < glob.selectionRadius)
                        {
                                matched = true;
                            break;
                        }
                    }
                    if (matched) break;
                }
                if (matched) break;
            }

            if (matched)
                netSelector = segmentList[ii].net;
            else
            netSelector = 0;

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime {0}",ts.Milliseconds );

            return netSelector;
        }

        public via findViaAt( PointF thisPoint )
        {   /// Finds a <see cref="via"/> at this board location</summary>
            for (int i=0; i<viaList.Count(); i++)
            {
                if (distanceBetweenPoints(thisPoint,viaList[i].location) < glob.selectionRadius ) return viaList[i];
            }
            return new via(0,0,new PointF(-99999f, -999999f));
        }
        public int findViaIndexAt(PointF thisPoint)
        {
            for (int i = 0; i < viaList.Count(); i++)
            {
                if (distanceBetweenPoints(thisPoint, viaList[i].location) < glob.selectionRadius ) return i;
            }
            return -1;
        }



        public static void causeException() { causeException("Not specified"); }
        public static void causeException(string errorMessage) //KLUDGE development only
        {
            Console.WriteLine("Exception thrown: {0}", errorMessage);
            object o2 = null;
            int i2 = (int)o2;   // Error
        }
        public static int GetEnumFromDescription(string description, Type enumType)
        {   // not my code - found at https://www.codegrepper.com/code-examples/whatever/get+enum+from+enum+description
            foreach (var field in enumType.GetFields())
            {
                DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute == null)
                    continue;
                if (attribute.Description == description)
                {
                    return (int)field.GetValue(null);
                }
            }
            return 0;
        }

    }

}
