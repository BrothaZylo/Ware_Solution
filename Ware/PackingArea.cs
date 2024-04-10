using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware;

public class PackingArea
{
    private readonly List<Pallet> pallets = new List<Pallet>();

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

    public void ReceivePackage(Package package)
    {
        Pallet? targetPallet = pallets.FindLast(p => !p.IsPalletFull());

        if (targetPallet == null)
        {
            targetPallet = new Pallet();
            pallets.Add(targetPallet);
        }

        targetPallet.AddPackageToPallet(package);
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
