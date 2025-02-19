﻿namespace Models.Dto.Addresses.Input
{
    public class UpdateAddressesDto
    {
        public IEnumerable<AddressCreateDto> CreatedAddresses { get; set; }
        public IEnumerable<AddressEditDto> EditedAddresses { get; set; }
        public IEnumerable<AddressDeleteDto> DeletedAddresses { get; set; }
    }
}
