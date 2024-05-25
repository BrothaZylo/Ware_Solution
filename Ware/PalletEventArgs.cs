using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Packages;

namespace Ware
{
    /// <summary>
    /// Pallet events
    /// </summary>
    public class PalletEventArgs
    {
        /// <summary>
        /// Pallet object
        /// </summary>
        public Pallet? Pallet { get; private set; }

        /// <summary>
        /// PalltStorage object
        /// </summary>
        public PalletStorage? PalletStorage { get; private set; }

        /// <summary>
        /// Package object
        /// </summary>
        public Package? Package { get; private set; }

        /// <summary>
        /// text value
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Empty event
        /// </summary>
        public PalletEventArgs()
        {
        }

        /// <summary>
        /// Pallet event containing pallet object
        /// </summary>
        /// <param name="pallet">pallet object</param>
        public PalletEventArgs(Pallet? pallet)
        {
            Pallet = pallet;
        }

        /// <summary>
        /// Pallet event containing pallet and palletstorage objects
        /// </summary>
        /// <param name="pallet">pallet object</param>
        /// <param name="palletStorage">palletstorage object</param>
        public PalletEventArgs(Pallet? pallet, PalletStorage? palletStorage) 
        {  
            Pallet = pallet;
            PalletStorage = palletStorage;
        }

        /// <summary>
        /// Pallet event containing pallet and package objects
        /// </summary>
        /// <param name="pallet">pallet object</param>
        /// <param name="package">packag object</param>
        public PalletEventArgs(Pallet pallet, Package? package)
        {
            Pallet = pallet;
            Package = package;
        }

        /// <summary>
        /// Pallet vent containing palltstorage and package objects
        /// </summary>
        /// <param name="palletStorage">palletstorage object</param>
        /// <param name="package">package object</param>
        public PalletEventArgs(PalletStorage? palletStorage, Package? package)
        {
            PalletStorage = palletStorage;
            Package = package;
        }

        /// <summary>
        /// Pallet event containing pallet, palletstorage and packag objects
        /// </summary>
        /// <param name="pallet">pallet object</param>
        /// <param name="palletStorage">palletstorage object</param>
        /// <param name="package">package object</param>
        public PalletEventArgs(Pallet? pallet, PalletStorage? palletStorage, Package? package)
        {
            Package = package;
            Pallet = pallet;
            PalletStorage = palletStorage;
        }
    }
}
