using System;

namespace CAMS.Module.Classi.Effemeridi
{

    public class Degree
    {
        public char Direction { get; set; }

        public int Deg { get; set; }
        public int Min { get; set; }
        public int Sec { get; set; }
    }
    public static class Utils
    {

        public static Degree ToSessLongitude(this double lon)
        {

            char lonDir = (lon >= 0 ? 'E' : 'W');
            lon = Math.Abs(lon);
            double lonMinPart = ((lon - Math.Truncate(lon) / 1) * 60);
            double lonSecPart = ((lonMinPart - Math.Truncate(lonMinPart) / 1) * 60);

            return new Degree()
            {
                Direction = lonDir,
                Deg = (int)lon,
                Min = (int)lonMinPart,
                Sec = (int)lonSecPart
            };
        }


        public static Degree ToSessLatitude(this double lat)
        {
            char latDir = (lat >= 0 ? 'N' : 'S');
            lat = Math.Abs(lat);
            double latMinPart = ((lat - Math.Truncate(lat) / 1) * 60);
            double latSecPart = ((latMinPart - Math.Truncate(latMinPart) / 1) * 60);


            return new Degree()
            {
                Direction = latDir,
                Deg = (int)lat,
                Min = (int)latMinPart,
                Sec = (int)latSecPart
            };
        }
    }

}
