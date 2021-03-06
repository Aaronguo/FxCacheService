﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fx.Domain.Base.IService;
using Fx.Domain.FxAggregate.IService;
using Fx.Domain.FxGoods;
using Fx.Domain.FxGoods.Search;
using Fx.Entity.FxGoods;
using Fx.Infrastructure.Caching;

namespace FxCacheService.FxGoods
{
    public partial class GoodsCache : BaseCache
    {
        protected IHomeSearch<GoodsTransferInfo> homeSearch;
        protected IHomeTopShow homeTopShow;
        protected GoodsListService goodsListService;
        protected ITopShow topShow;
        protected GoodsBuySearchService goodsBuySearchService;
        protected GoodsTransferSearchService goodsTransferSearchService;
        public GoodsCache(IHomeSearch<GoodsTransferInfo> homeSearch,
            IHomeTopShow homeTopShow,
            GoodsListService goodsListService,
            ITopShow topShow,
            GoodsBuySearchService goodsBuySearchService,
            GoodsTransferSearchService goodsTransferSearchService)
        {
            this.homeSearch = homeSearch;
            this.homeTopShow = homeTopShow;
            this.goodsListService = goodsListService;
            this.topShow = topShow;
            this.goodsBuySearchService = goodsBuySearchService;
            this.goodsTransferSearchService = goodsTransferSearchService;
        }

        public List<GoodsTransferInfo> GetHomeLatest()
        {
            int number = 10;
            if (cacheService.Get(CacheKey.GoodsKey.GOODS_HOME_TRANSFER_LATEST) == null)
            {
                var list = homeSearch.SearchLatestForHome(number);
                CheckGoodsList(list);
                cacheService.Insert(CacheKey.GoodsKey.GOODS_HOME_TRANSFER_LATEST, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);              
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODS_HOME_TRANSFER_LATEST) as List<GoodsTransferInfo>;
        }

        public List<GoodsTransferInfo> GetHomeTopShow()
        {
            if (cacheService.Get(CacheKey.GoodsKey.GOODS_HOME_TOP_SHOW_LATEST) == null)
            {
                var list = homeTopShow.GetHomeGoodsTopShow();
                CheckGoodsList(list);
                cacheService.Insert(CacheKey.GoodsKey.GOODS_HOME_TOP_SHOW_LATEST, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODS_HOME_TOP_SHOW_LATEST) as List<GoodsTransferInfo>;
        }


        public List<GoodsTransferInfo> GetGoodsElectronicsList()
        {
            if (cacheService.Get(CacheKey.GoodsKey.GOODSLIST_Electronics) == null)
            {
                var list = goodsListService.Electronics();
                cacheService.Insert(CacheKey.GoodsKey.GOODSLIST_Electronics, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODSLIST_Electronics) as List<GoodsTransferInfo>;
        }

        public List<GoodsTransferInfo> GetGoodsHomeSuppliesList()
        {
            if (cacheService.Get(CacheKey.GoodsKey.GOODSLIST_HomeSupplies) == null)
            {
                var list = goodsListService.HomeSupplies();
                cacheService.Insert(CacheKey.GoodsKey.GOODSLIST_HomeSupplies, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODSLIST_HomeSupplies) as List<GoodsTransferInfo>;
        }

        public List<GoodsTransferInfo> GetGoodsFashionList()
        {
            if (cacheService.Get(CacheKey.GoodsKey.GOODSLIST_Fashion) == null)
            {
                var list = goodsListService.Fashion();
                cacheService.Insert(CacheKey.GoodsKey.GOODSLIST_Fashion, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODSLIST_Fashion) as List<GoodsTransferInfo>;
        }

        public List<GoodsTransferInfo> GetGoodsCultureLifeList()
        {
            if (cacheService.Get(CacheKey.GoodsKey.GOODSLIST_CultureLife) == null)
            {
                var list = goodsListService.CultureLife();
                cacheService.Insert(CacheKey.GoodsKey.GOODSLIST_CultureLife, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODSLIST_CultureLife) as List<GoodsTransferInfo>;
        }

        public List<GoodsTransferInfo> GetGoodsOtherList()
        {
            if (cacheService.Get(CacheKey.GoodsKey.GOODSLIST_Other) == null)
            {
                var list = goodsListService.Other();
                cacheService.Insert(CacheKey.GoodsKey.GOODSLIST_Other, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODSLIST_Other) as List<GoodsTransferInfo>;
        }



        public List<GoodsTransferInfo> GetGoodsTransferTopShow()
        {
            if (cacheService.Get(CacheKey.GoodsKey.GOODS_TRANSFER_TOPSHOW) == null)
            {
                var list = topShow.GetGoodsTransferTopShow();
                cacheService.Insert(CacheKey.GoodsKey.GOODS_TRANSFER_TOPSHOW, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODS_TRANSFER_TOPSHOW) as List<GoodsTransferInfo>;
        }

        public List<GoodsBuyInfo> GetGoodsBuyTopShow()
        {
            if (cacheService.Get(CacheKey.GoodsKey.GOODS_BUY_TOPSHOW) == null)
            {
                var list = topShow.GetGoodsBuyTopShow();
                cacheService.Insert(CacheKey.GoodsKey.GOODS_BUY_TOPSHOW, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODS_BUY_TOPSHOW) as List<GoodsBuyInfo>;
        }



        public List<GoodsBuyInfo> GetMainGoodsBuyALL()
        {
            int count = 100;
            if (cacheService.Get(CacheKey.GoodsKey.GOODS_BUY_GetMainGoodsALL) == null)
            {
                var list = goodsBuySearchService.SearchByKey("", 0, count);
                cacheService.Insert(CacheKey.GoodsKey.GOODS_BUY_GetMainGoodsALL, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODS_BUY_GetMainGoodsALL) as List<GoodsBuyInfo>;
        }

        public List<GoodsBuyInfo> GetMainGoodsBuyOnlyChangeGoods()
        {
            int count = 100;
            if (cacheService.Get(CacheKey.GoodsKey.GOODS_BUY_GetMainGoodsOnlyChangeGoods) == null)
            {
                var list = goodsBuySearchService.SearchByKey("", 0, 0, 0, count);
                cacheService.Insert(CacheKey.GoodsKey.GOODS_BUY_GetMainGoodsOnlyChangeGoods, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODS_BUY_GetMainGoodsOnlyChangeGoods) as List<GoodsBuyInfo>;
        }
        public List<GoodsBuyInfo> GetMainGoodsBuyOnlyPrice()
        {
            int count = 100;
            if (cacheService.Get(CacheKey.GoodsKey.GOODS_BUY_GetMainGoodsOnlyPrice) == null)
            {
                var list = goodsBuySearchService.SearchByKey("", 0, 0, 0, count);
                cacheService.Insert(CacheKey.GoodsKey.GOODS_BUY_GetMainGoodsOnlyPrice, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODS_BUY_GetMainGoodsOnlyPrice) as List<GoodsBuyInfo>;
        }

        public List<GoodsTransferInfo> GetMainGoodsTransferALL()
        {
            int count = 100;
            if (cacheService.Get(CacheKey.GoodsKey.GOODS_TRANSFER_GetMainGoodsALL) == null)
            {
                var list = goodsTransferSearchService.SearchByKey("", 0, count);
                cacheService.Insert(CacheKey.GoodsKey.GOODS_TRANSFER_GetMainGoodsALL, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODS_TRANSFER_GetMainGoodsALL) as List<GoodsTransferInfo>;
        }

        public List<GoodsTransferInfo> GetMainGoodsTransferOnlyChangeGoods()
        {
            int count = 100;
            if (cacheService.Get(CacheKey.GoodsKey.GOODS_TRANSFER_GetMainGoodsOnlyChangeGoods) == null)
            {
                var list = goodsTransferSearchService.SearchWhenChangeGoods(0, count);
                cacheService.Insert(CacheKey.GoodsKey.GOODS_TRANSFER_GetMainGoodsOnlyChangeGoods, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODS_TRANSFER_GetMainGoodsOnlyChangeGoods) as List<GoodsTransferInfo>;
        }

        public List<GoodsTransferInfo> GetMainGoodsTransferOnlyPrice()
        {
            int count = 100;
            if (cacheService.Get(CacheKey.GoodsKey.GOODS_TRANSFER_GetMainGoodsOnlyPrice) == null)
            {
                var list = goodsTransferSearchService.SearchWhenPrice(0, count);
                cacheService.Insert(CacheKey.GoodsKey.GOODS_TRANSFER_GetMainGoodsOnlyPrice, list, cacheHalfHour, System.Web.Caching.CacheItemPriority.Normal);
            }
            return cacheService.Get(CacheKey.GoodsKey.GOODS_TRANSFER_GetMainGoodsOnlyPrice) as List<GoodsTransferInfo>;
        }


        private void CheckGoodsList(List<GoodsTransferInfo> infos)
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
