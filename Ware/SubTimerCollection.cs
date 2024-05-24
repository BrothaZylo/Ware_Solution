using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ware.TimeEstimate;

namespace Ware
{
    /// <summary>
    /// Subclass for list inputs
    /// </summary>
    public class SubTimerCollection
    {
        private int fromShelf;
        private int toShelf;
        private int timeSeconds;
        private PlaceOrGetBox placeOrGetBox;

        /// <summary>
        /// enum PlaceOrGetBox
        /// </summary>
        public PlaceOrGetBox PlaceOrGet
        {
            get { return placeOrGetBox; }
            set { placeOrGetBox = value; }
        }

        /// <summary>
        /// Shelf number
        /// </summary>
        public int FromShelf
        {
            get { return fromShelf; }
            set { fromShelf = value; }
        }
        /// <summary>
        /// Shelf number
        /// </summary>
        public int ToShelf
        {
            get { return toShelf; }
            set { toShelf = value; }
        }
        /// <summary>
        /// Time it takes to get a package
        /// </summary>
        public int TimeSeconds
        {
            get { return timeSeconds; }
            set { timeSeconds = value; }
        }
    }
}
