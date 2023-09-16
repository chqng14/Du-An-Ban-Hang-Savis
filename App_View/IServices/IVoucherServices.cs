using App_Data.Models;

namespace App_View.IServices
{
    public interface IVoucherServices
    {
        public Task<List<Voucher>> GetAllAsync();
        public Task<Voucher> GetVoucherAsync(string item);
        public Task<bool> AddVoucherAsync(Voucher item);
        public Task<bool> RemoveVoucher(Voucher item);
        public Task<bool> EditVoucher(Voucher item);
    }
}
