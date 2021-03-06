﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavRTK.ModuleGPS.Model
{
    #region ObjectGP class
    /// <summary>
    /// ObjectGP father of ObjectGPGGA and Object GPRMC
    /// </summary>
    public class ObjectGP
    {
        #region ObjectGP Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ObjectGP()
        {
        }
        #endregion

        #region Method toLatitude
        /// <summary>
        /// Used to initilize latitude from string parameter
        /// </summary>
        /// <param name="lat">string to latitude</param>
        /// <returns>latitude</returns>
        public double toLatitude(string lat, char NorS)
        {
            int point;
            int deg = 0;
            double min = 0, sec = 0;
            int positiv = 1;

            if (NorS == 'S')
                positiv = -1;

            if (lat != String.Empty)
            {

                deg = int.Parse(lat.Substring(0, 2));
                min = double.Parse(lat.Substring(2, 2));
                point = lat.IndexOf('.');
                sec = double.Parse(lat.Substring(point + 1, lat.Length - point - 1)) * 6 / Math.Pow(10, (lat.Length - point - 2));

                return positiv * (deg + min / 60 + sec / 3600);



            }
            else return 0;
        }
        #endregion Method toLatitude


        #region Method toLongitude
        /// <summary>
        /// Used to initilize longitude from string parameter
        /// </summary>
        /// <param name="lon">string to latitude</param>
        /// <returns>longitude</returns>
        public double toLongitude(string lon, char EorW)
        {
            int point;
            int deg = 0;
            double min = 0, sec = 0;
            int positiv = 1;

            if (EorW == 'W')
                positiv = -1;

            if (lon != String.Empty)
            {
                deg = int.Parse(lon.Substring(0, 3));
                min = double.Parse(lon.Substring(3, 2));
                point = lon.IndexOf(".");
                sec = double.Parse(lon.Substring(point + 1, lon.Length - point - 1)) * 6 / Math.Pow(10, (lon.Length - point - 2));

                return positiv * (deg + min / 60 + sec / 3600);
            }
            else return 0;
        }
        #endregion Method toLongitude
    }
    #endregion
}
