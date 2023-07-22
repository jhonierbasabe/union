using System;
using OSGeo.OSR;

namespace GIS
{
    public class Proj4
    {
        public void transform()
        {
   
            GdalConfiguration.ConfigureOgr(); 
            try
            {
                SpatialReference src = new SpatialReference("");
                src.ImportFromProj4("+proj=latlong +datum=WGS84 +no_defs");
                Console.WriteLine("SOURCE IsGeographic:" + src.IsGeographic() + " IsProjected:" + src.IsProjected());
                SpatialReference dst = new SpatialReference("");
                dst.ImportFromProj4("+proj=latlong +datum=3116 +no_defs");
                Console.WriteLine("DEST IsGeographic:" + dst.IsGeographic() + " IsProjected:" + dst.IsProjected());
                /* -------------------------------------------------------------------- */
                /*      making the transform                                            */
                /* -------------------------------------------------------------------- */
                CoordinateTransformation ct = new CoordinateTransformation(src, dst);
                double[] p = new double[3];
                //4.127077, -73.607755
                p[0] = 4.127077; p[1] = -73.607755; p[2] = 0;
                ct.TransformPoint(p);
                Console.WriteLine("x:" + p[0] + " y:" + p[1] + " z:" + p[2]);
                ct.TransformPoint(p, 19.2, 47.5, 0);
                Console.WriteLine("x:" + p[0] + " y:" + p[1] + " z:" + p[2]);
            }

            catch (Exception e)
            {
                Console.WriteLine("Error occurred: " + e.Message);
                System.Environment.Exit(-1);
            }
        }
    }
}

