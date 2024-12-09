using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.VNPay;

namespace TourAPI.Interfaces.Service
{
    public interface IVNPayService
    {
        string CreatePaymentUrl(VNPayReqDto req, HttpContext context);
        VNPayResDto PaymentExecute(IQueryCollection collections);
    }
}