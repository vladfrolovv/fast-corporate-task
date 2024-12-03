using System;
using UniRx;
namespace Hazards
{
    public class AcidCloud : IDisposable
    {

        private readonly AcidDropsFactory _acidDropsFactory;
        private readonly AcidZone _acidZone;

        private readonly CompositeDisposable _compositeDisposable = new();

        public AcidCloud(AcidCloudConfig acidCloudConfig, AcidZone acidZone, AcidDropsFactory acidDropsFactory)
        {
            _acidZone = acidZone;
            _acidDropsFactory = acidDropsFactory;

            Observable.Interval(TimeSpan.FromSeconds(acidCloudConfig.RaindropsDelay)).Subscribe(delegate
            {
                CreteDrop();
            });
        }

        private AcidDrop CreteDrop()
        {
            AcidDrop drop = _acidDropsFactory.Create(new AcidDropInfo());
            drop.Position = _acidZone.GetPointInZone();
            drop.transform.parent = _acidZone.transform;

            return drop;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

    }
}
