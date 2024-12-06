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
        private readonly AcidCloudZone _acidCloudZone;
        private readonly ShadowsFactory _shadowsFactory;

        private readonly CompositeDisposable _compositeDisposable = new();

        public AcidCloud(AcidCloudConfig acidCloudConfig, AcidCloudZone acidCloudZone, AcidDropsFactory acidDropsFactory, ShadowsFactory shadowsFactory)
        {
            _acidCloudZone = acidCloudZone;
            _acidCloudConfig = acidCloudConfig;
            _acidDropsFactory = acidDropsFactory;
            _shadowsFactory = shadowsFactory;

            Observable
                .Interval(TimeSpan.FromSeconds(acidCloudConfig.RaindropsDelay))
                .Subscribe(delegate
                {
                    CreteDrop();
                }).AddTo(_compositeDisposable);
        }

        private void CreteDrop()
        {
            Vector3 startPoint = _acidCloudZone.GetPointInZone();
            Vector3 endPoint = new (startPoint.x, 0f, startPoint.z);

            AcidDrop drop = _acidDropsFactory.Create(new AcidDropInfo(
                endPoint,
                _acidCloudConfig.RaindropSpeedMultiplier
                ));
            Shadow shadow = _shadowsFactory.Create(new ShadowInfo(drop, startPoint));

            drop.transform.position = startPoint;
            shadow.transform.position = endPoint;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

    }
}
