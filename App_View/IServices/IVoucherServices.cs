using App_Data.Models;
using App_Data.ViewModels.Voucher;

namespace App_View.IServices
{
    public interface IVoucherServices
    {
        public Task<List<Voucher>> GetAllAsync();
        public Task<Voucher> GetVoucherAsync(string item);
        public Task<bool> AddVoucherAsync(VoucherDTO item);
        public Task<bool> RemoveVoucher(Guid item);
        public Task<bool> EditVoucher(VoucherDTO item);
        Task<VoucherDTO> GetVoucherDTOById(Guid id);
        Task<bool> UpdateVoucherAfterUseIt(Guid id);
    }
}
