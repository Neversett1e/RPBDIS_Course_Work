using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication3
{
    public enum PublisherSortState
    {
        NameAsc, NameDesc,
        CityAsc, CityDesc,
        AddressAsc, AddressDesc
    }

    public class PublisherSortViewModel
    {
        public PublisherSortState NameSort { get; }
        public PublisherSortState CitySort { get; }
        public PublisherSortState AddressSort { get; }

        public PublisherSortState Current { get; }

        public PublisherSortViewModel(PublisherSortState state)
        {
            NameSort = state == PublisherSortState.NameAsc ? PublisherSortState.NameDesc : PublisherSortState.NameAsc;
            CitySort = state == PublisherSortState.CityAsc ? PublisherSortState.CityDesc : PublisherSortState.CityAsc;
            AddressSort = state == PublisherSortState.AddressAsc ? PublisherSortState.AddressDesc : PublisherSortState.AddressAsc;

            Current = state;
        }
    }
}
