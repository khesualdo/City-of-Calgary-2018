using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace City_of_Calgary_2018
{

    // Represent's a single geo-coord
    class Point {
        public double x { get; set; }
        public double y { get; set; }

        public Point(double x, double y) {
            this.x = x;
            this.y = y;
        }
    }

    // Parses the JSON file and retrieves the community params based on
    // geo-coords lonInput and latInput
    class DataLayer
    {

        public string communityName = "None";
        public string communitySector = "None";
        public string communityType = "None";
        bool foundCommunity = false;

        public void getData(String fileName, double lonInput, double latInput) 
        {

            using (StreamReader r = new StreamReader(fileName))
            {
                // Fetch JSON data
                string json = r.ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(json);
                
                // Scan the communities
                foreach (dynamic community in data.data)
                {
                    dynamic currentCommunity = community;
                    dynamic type = community[9];
                    
                    // Only ignore rural communities
                    if (type == "Residential" || 
                    type == "Industrial" || 
                    type == "Major Park") {

                        // Clean-up polygon data
                        string polygon = community[8];
                        polygon = polygon.Substring(0, polygon.Length - 2); // Get rid of the last 2 chars
                        polygon = polygon.Substring(10); // Get rid of the first 10 chars

                        // Split polygon string into lon and lat pairs
                        string[] polygonCoord = polygon.Split(',');
                        Point[] polygonShape = new Point[polygonCoord.Length];
                        int count = 0;

                        foreach (string coordList in polygonCoord) {

                            // eg1. coordList = " -114.070768950013 50.853339905893"
                            // eg2. coordList = "-114.070768950013 50.853339905893"
                            string coordListNew = "";
                            if (coordList[0].Equals(' ')) {
                                coordListNew = coordList.Substring(1);
                            } else {
                                coordListNew = coordList;
                            }

                            string[] coord = coordListNew.Split(' ');
                            double lon = Double.Parse(coord[0]); // -114.070768950013
                            double lat = Double.Parse(coord[1]); // 50.853339905893

                            polygonShape[count++] = new Point(lon, lat);
                        }

                        // Point of interest
                        Point statueLocation = new Point(lonInput, latInput);

                        // Check if point is within this community
                        Intersect intersect = new Intersect();
                        bool result = intersect.isInside(polygonShape, polygonCoord.Length, statueLocation);

                        // If we found the correct community
                        // update variable values and return
                        if (result) {
                            foundCommunity = true;
                            communityName = currentCommunity[12];
                            communityType = currentCommunity[13];
                            communitySector = currentCommunity[9];

                            return;
                        }
                    }
                }
            }
        }
    }

    // Implementation of the Point Inclusion algorithm
    // https://www.geeksforgeeks.org/how-to-check-if-a-given-point-lies-inside-a-polygon/
    class Intersect
    {

        public int infinity = int.MaxValue;

        // Given three colinear points p, q, r, the function checks if
        // point q lies on line segment 'pr'
        bool onSegment(Point p, Point q, Point r)
        {
            if (q.x <= Math.Max(p.x, r.x) && q.x >= Math.Min(p.x, r.x) &&
            q.y <= Math.Max(p.y, r.y) && q.y >= Math.Min(p.y, r.y))
            {
                return true;
            }
            return false;
        }

        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        int orientation(Point p, Point q, Point r)
        {
            double val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);

            // colinear
            if (val == 0)
            {
                return 0;
            }

            // clock or counterclock wise
            if (val > 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        // The function that returns true if line segment 'p1q1'
        // and 'p2q2' intersect.
        bool doIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            // Find the four orientations needed for general and
            // special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }

            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1))
            {
                return true;
            }

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1))
            {
                return true;
            }

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2))
            {
                return true;
            }

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2))
            {
                return true;
            }

            return false; // Doesn't fall in any of the above cases
        }

        // Returns true if the point p lies inside the polygon[] with n vertices
        public bool isInside(Point[] polygon, int n, Point p)
        {
            // There must be at least 3 vertices in polygon[]
            if (n < 3) return false;

            // Create a point for line segment from p to infinite
            Point extreme = new Point(infinity, p.y);

            // Count intersections of the above line with sides of polygon
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to 'extreme' intersects
                // with the line segment from 'polygon[i]' to 'polygon[next]'
                if (doIntersect(polygon[i], polygon[next], p, extreme))
                {
                    // If the point 'p' is colinear with line segment 'i-next',
                    // then check if it lies on segment. If it lies, return true,
                    // otherwise false
                    if (orientation(polygon[i], p, polygon[next]) == 0)
                        return onSegment(polygon[i], p, polygon[next]);

                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise
            return (count % 2 == 1);
        }
    }
}
