using System;
using Hazards.AcidDrops;
using Hazards.Targets;
using UniRx;
using UnityEngine;
namespace Hazards
{
    public class AcidCloud : IDisposable
    {

        private readonly AcidDropsFactory _acidDropsFactory;
        private readonly AcidCloudConfig _acidCloudConfig;
        private readonly AcidZone _acidZone;
        private readonly TargetsFactory _targetsFactory;

        private readonly CompositeDisposable _compositeDisposable = new();

        public AcidCloud(AcidCloudConfig acidCloudConfig, AcidZone acidZone, AcidDropsFactory acidDropsFactory, TargetsFactory targetsFactory)
        {
            _acidZone = acidZone;
            _acidCloudConfig = acidCloudConfig;
            _acidDropsFactory = acidDropsFactory;
            _targetsFactory = targetsFactory;

            Observable
                .Interval(TimeSpan.FromSeconds(acidCloudConfig.RaindropsDelay))
                .Subscribe(delegate
                {
                    CreteDrop();
                }).AddTo(_compositeDisposable);
        }

        private void CreteDrop()
        {
            Vector3 startPoint = _acidZone.GetPointInZone();
            Vector3 destinationPoint = new (startPoint.x, 0f, startPoint.z);
            AcidDrop drop = _acidDropsFactory.Create(new AcidDropInfo(
                destinationPoint,
                _acidCloudConfig.RaindropSpeedMultiplier
                ));

            Target target = _targetsFactory.Create(new TargetInfo());
            target.transform.position = destinationPoint + Vector3.up * .01f;

            drop.transform.position = startPoint;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

    }
}
