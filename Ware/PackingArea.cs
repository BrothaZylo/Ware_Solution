using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware;

public class PackingArea
{
    private readonly List<Pallet> pallets = new List<Pallet>();
    private readonly List<Package> unallocatedPackages = new List<Package>();

    /// <summary>
    /// Adds a package to a pallet. If the pallet is full a exception is thrown.
    /// </summary>
    /// <param name="package">The package to add.</param>
    /// <param name="pallet">The pallet to add the package to.</param>
    public void AddToPallet(Package package, Pallet pallet)
    {
        if (!pallet.IsPalletFull())
        {
            pallet.AddPackageToPallet(package);
            RaiseAddToPalletEvent(package);
        }
        else
        {
            throw new InvalidOperationException("Pallet is full, can't add more packages.");
        }
    }

    /// <summary>
    /// Receives a package and stores it in the packing area storage.
    /// </summary>
    /// <param name="package">The package to be stored.</param>
    public void ReceivePackage(Package package)
    {
        unallocatedPackages.Add(package);
    }

    /// <summary>
    /// Allocates stored packages to pallets. Creates new pallets if necessary.
    /// </summary>
    public void AllocatePackagesToPallets()
    {
        foreach (Package package in unallocatedPackages)
        {
            Pallet? targetPallet = null;
            for (int i = pallets.Count - 1; i >= 0; i--)
            {
                if (!pallets[i].IsPalletFull())
                {
                    targetPallet = pallets[i];
                    break;
                }
            }

            if (targetPallet == null)
            {
                targetPallet = new Pallet();
                pallets.Add(targetPallet);
            }

            targetPallet.AddPackageToPallet(package);
        }

        unallocatedPackages.Clear();
    }

    public IReadOnlyList<Pallet> Pallets
    {
        get { return pallets.AsReadOnly(); }
    }

    public event EventHandler<PackageEventArgs> PackageAddedToPallet;

    private void RaiseAddToPalletEvent(Package package)
    {
        PackageAddedToPallet?.Invoke(this, new PackageEventArgs(package));
    }
}