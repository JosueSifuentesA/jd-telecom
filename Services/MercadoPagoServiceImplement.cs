using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Mvc;

namespace JDTelecomunicaciones.Services
{
    public class MercadoPagoServiceImplement
    {

         public async Task<object> CrearPago(dynamic responseData,string descripcion){

            try{

                MercadoPagoConfig.AccessToken = "TEST-5146615863179763-100401-ea6a000b35409cfad3e6199784028ba3-1444186216";

                var types = Convert.ToDecimal(responseData.transaction_amount);
                Type type = types.GetType();

                Console.WriteLine("El tipo de valor de transaction_amount es: " + type);

                Console.WriteLine($"{responseData.transaction_amount} - token: {responseData.token} - descripcion: {descripcion} - installments: {responseData.installments} - paymentMethodId: {responseData.payment_method_id} - email: {responseData.payer.email} - type: {responseData.payer.identification.type} - number: {responseData.payer.identification.number}- {responseData.issuer_id}");

                PaymentCreateRequest paymentRequest = new PaymentCreateRequest
                {
                    TransactionAmount = Convert.ToDecimal(responseData.transaction_amount),
                    Token =responseData.token,
                    Description = descripcion,
                    Installments = Convert.ToInt32(responseData.installments),
                    PaymentMethodId = responseData.payment_method_id,
                    Payer = new PaymentPayerRequest
                    {
                        Email = responseData.payer.email,
                        Identification = new IdentificationRequest
                        {
                            Type = responseData.payer.identification.type,
                            Number = responseData.payer.identification.number,
                        },
                        FirstName = responseData.payer.FirstName
                    },
                    IssuerId = responseData.issuer_id,
                    //NotificationUrl = responseData.issuer_id ,
                    CallbackUrl = "www.google.com"
                };


                var client = new PaymentClient();
                Payment payment = await client.CreateAsync(paymentRequest);
                Console.WriteLine($"{payment.CallbackUrl} - {payment.StatusDetail}");
                return payment;

            }catch(Exception e){
                Console.WriteLine($"ERROR:{e.HResult} - {e.Message}");
                return "error";
            }
        }



    }
}