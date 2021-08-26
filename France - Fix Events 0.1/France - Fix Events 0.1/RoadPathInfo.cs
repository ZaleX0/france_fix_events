using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace France___Fix_Events_0._1
{
    class RoadPathInfo
    {
        private string path;

        public RoadPathInfo(string path)
        {
            this.path = path;
        }

        public string RoadSectionName
        {
            get
            {
                string[] pathSplits = path.Split('\\');

                string a = pathSplits[pathSplits.Length - 3].Split('_')[0];
                string[] b = pathSplits[pathSplits.Length - 2].Split('_');

                return $"{a}-{b[0]}-{b[1]}";
            }
        }
        public int RoadNumber
        {
            get
            {
                string[] pathSplits = path.Split('\\');
                return int.Parse(pathSplits[pathSplits.Length - 3].Split('_')[1]);
            }
        }
        public int RoadSide
        {
            get
            {
                string[] pathSplits = path.Split('\\');
                string side = pathSplits[pathSplits.Length - 3].Split('_')[2];
                if (side == "R")
                    return 1;
                else
                    return -1;
            }
        }
        public int RoadLaneNumber
        {
            get
            {
                if (RoadSide < 0)
                    return 2;
                else
                    return 1;

                //string[] pathSplits = path.Split('\\');
                //return int.Parse(pathSplits[pathSplits.Length - 3].Split('_')[3]);
            }
        }
        public int RoadMeter
        {
            get
            {
                string[] pathSplits = path.Split('\\');
                return int.Parse(pathSplits[pathSplits.Length - 1].Substring(5, 4));
            }
        }

    }
}
