﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fx.Domain.Base.IService;
using Fx.Domain.FxAggregate.IService;
using Fx.Domain.FxCar;
using Fx.Domain.FxCar.Search;
using Fx.Entity.FxCar;
using Fx.Entity.FxGoods;
using Fx.Entity.FxHouse;
using Fx.Infrastructure.Caching;

namespace FxCacheService.FxCar
{
    public partial class CarCache : BaseCache
    {
        protected IHomeSearch<CarTransferInfo> homeSearch;
        protected IHomeTopShow homeTopShow;
        protected CarListService carListService;
        protected ITopShow topShow;
        protected CarBuySearchService carBuySearchService;
        protected CarTransferSearchService carTransferSearchService;
        public CarCache(IHomeSearch<CarTransferInfo> homeSearch,
            IHomeTopShow homeTopShow,
            CarListService carListService,
            ITopShow topShow,
            CarBuySearchService carBuySearchService,
            CarTransferSearchService carTransferSearchService)
        {
            this.homeSearch = homeSearch;
            this.homeTopShow = homeTopShow;
            this.carListService = carListService;
            this.topShow = topShow;
            this.carBuySearchService = carBuySearchService;
            this.carTransferSearchService = carTransferSearchService;

        }

        public List<CarTransferInfo> GetHomeLatest()
        {
            int number = 10;
            if (cacheService.Get(CacheKey.CarKey.CAR_HOME_TRANSFER_LATEST) == null)
            {
                var list = homeSearch.SearchLatestForHome(number);
                CheckCarList(list);
                cacheService.Insert(CacheKey.CarKey.CAR_HOME_TRANSFER_LATEST, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.CarKey.CAR_HOME_TRANSFER_LATEST) as List<CarTransferInfo>;
        }


        public List<CarTransferInfo> GetHomeTopShow()
        {
            if (cacheService.Get(CacheKey.CarKey.CAR_HOME_TOP_SHOW_LATEST) == null)
            {
                var list = homeTopShow.GetHomeCarTopShow();
                CheckCarList(list);
                cacheService.Insert(CacheKey.CarKey.CAR_HOME_TOP_SHOW_LATEST, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.CarKey.CAR_HOME_TOP_SHOW_LATEST) as List<CarTransferInfo>;
        }

        public List<CarTransferInfo> GetCarSecondHandCarList()
        {
            if (cacheService.Get(CacheKey.CarKey.CARLIST_SecondHandCar) == null)
            {
                var list = carListService.GetSecondHandCar();
                cacheService.Insert(CacheKey.CarKey.CARLIST_SecondHandCar, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.CarKey.CARLIST_SecondHandCar) as List<CarTransferInfo>;
        }


        public List<CarTransferInfo> GetCarTransferTopShow()
        {
            if (cacheService.Get(CacheKey.CarKey.CAR_TRANSFER_TOPSHOW) == null)
            {
                var list = topShow.GetCarTransferTopShow();
                cacheService.Insert(CacheKey.CarKey.CAR_TRANSFER_TOPSHOW, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.CarKey.CAR_TRANSFER_TOPSHOW) as List<CarTransferInfo>;
        }


        public List<CarBuyInfo> GetCarBuyTopShow()
        {
            if (cacheService.Get(CacheKey.CarKey.CAR_BUY_TOPSHOW) == null)
            {
                var list = topShow.GetCarBuyTopShow();
                cacheService.Insert(CacheKey.CarKey.CAR_BUY_TOPSHOW, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.CarKey.CAR_BUY_TOPSHOW) as List<CarBuyInfo>;
        }


        public List<CarBuyInfo> GetMainCarBuyALL()
        {
            int count = 100;
            if (cacheService.Get(CacheKey.CarKey.CAR_BUY_GetMainCARALL) == null)
            {
                var list = carBuySearchService.SearchByKey("", 0, count);
                cacheService.Insert(CacheKey.CarKey.CAR_BUY_GetMainCARALL, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.CarKey.CAR_BUY_GetMainCARALL) as List<CarBuyInfo>;
        }
        

        public List<CarTransferInfo> GetMainTransferBuyALL()
        {
            int count = 100;
            if (cacheService.Get(CacheKey.CarKey.CAR_TRANSFER_GetMainCARALL) == null)
            {
                var list = carTransferSearchService.SearchByKey("", 0, count);
                cacheService.Insert(CacheKey.CarKey.CAR_TRANSFER_GetMainCARALL, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.CarKey.CAR_TRANSFER_GetMainCARALL) as List<CarTransferInfo>;
        }

        private void CheckCarList(List<CarTransferInfo> infos)
        {
            if (infos != null)
            {
                foreach (var item in infos)
                {
                    item.PublishTitle = Fx.Infrastructure.Data.Cut.CutStr(item.PublishTitle, 16);
                }
            }
        }
    }
}
