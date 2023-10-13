$(document).ready(function() {
         const mp = new MercadoPago('TEST-aa3fee75-b76d-47b1-89f5-5c4bdd117727', {
               locale: 'es-AR'
            });

         const bricksBuilder = mp.bricks();
         const renderCardPaymentBrick = async (bricksBuilder,totalPrice) => {
               console.log('The total price is ' + totalPrice)
               const settings = {
                  initialization: {
                     amount: totalPrice, // monto a ser pago
                     payer: {
                        email: "test@test.com",
                     },
                  },
                  customization: {
                     visual: {
                        style: {
                           theme: 'default', // | 'dark' | 'bootstrap' | 'flat'
                        }
                     },
                     paymentMethods: {
                        maxInstallments: 1,
                     }
                  },
                  callbacks: {
                     onReady: () => {
                        console.log("Brick ready!")
                     },
                     onSubmit: (cardFormData) => {
            
                        fetch("/process_payment", {
                          method: "POST",
                          headers: {
                            "Content-Type": "application/json",
                          },
                          body: JSON.stringify(cardFormData),
                        })
                          .then((response) => {
                           if (!response.ok) {
                              throw new Error("La solicitud no se completó correctamente");
                           }
                           console.log(response)
                           return response.json();
                          })
                          .then((data) => {
                              console.log(typeof(data));
                              var vouchersPaymentInfo = getVouchersPaymentInfo()
                              paymentInfo = {
                              data : data,
                              voucherId : vouchersPaymentInfo.inputIdsArray
                              }
                              console.log(paymentInfo)
                              console.log(`${data.transactionAmount} - ${data.id}`);
                              fetch("/pagar_recibos",{
                                 method: "POST",
                                 headers: {
                                    "Content-Type": "application/json",
                                 },
                                 body: JSON.stringify(paymentInfo),
                              }).then((response2)=>{
                                 console.log(response2);
                              })
                            //return data; // Resuelve la promesa con los datos
                          })
                          .catch((error) => {
                            console.error(error);
                            throw error; // Rechaza la promesa con el error
                          });
                        
                     },
                     onError: (error) => {
                     },
                  },
               };
               window.cardPaymentBrickController = await bricksBuilder.create('cardPayment', 'cardPaymentBrick_container', settings);
               //window.cardPaymentBrickController = await bricksBuilder.closest();
               $('.popUpPaymentContainer').css('display','flex')
         };         
         function obtenerVouchersSeleccionados() {
            var vouchersSeleccionados = [];

            
            $('.voucherHandler_voucher input[type="checkbox"]:checked').each(function() {
               var $voucherContainer = $(this).closest('.voucherHandler_voucher');
               var voucherId = $voucherContainer.find('label:first').text(); 
               var voucherDate = $voucherContainer.find('label:nth-child(3)').text(); 
               var voucherPrice = $voucherContainer.find('label:last').text();

                  
                  var voucher = {
                     id: voucherId,
                     precio: voucherPrice,
                     fechaVencimiento: voucherDate
                  };

                  vouchersSeleccionados.push(voucher);
         });

        return vouchersSeleccionados;
    }


         $('#btnPagarRecibo').click(function (event){
            event.preventDefault(); 
            payVoucher();
         });

         const payVoucher = () =>{
            const vouchersSeleccionados = getVouchersPaymentInfo();
            renderCardPaymentBrick(bricksBuilder,vouchersSeleccionados.inputTotalPriceAmmount);

         }
         const getVouchersPaymentInfo = ( ) =>{
            var vouchersSeleccionados = obtenerVouchersSeleccionados();
            if(vouchersSeleccionados.length == 0){
               return
            }

            var inputIdsArray = [];
            var prices = [];


            for (var i = 0; i < vouchersSeleccionados.length; i++) {
                inputIdsArray.push(parseInt(vouchersSeleccionados[i].id));
                prices.push(parseFloat(vouchersSeleccionados[i].precio.slice(2)))
            }

            console.log(inputIdsArray);
            console.log(prices);

            var inputIdsJson = JSON.stringify(inputIdsArray);
            var inputPriceJson = JSON.stringify(prices);
            
            let priceTotalAmmount = 0;
            for(let i = 0 ; i<prices.length;i++){

               priceTotalAmmount = priceTotalAmmount + prices[i]; 
            
            }

            return paymentInfo  = {
               inputIdsArray :inputIdsArray,
               inputPrice : inputPriceJson,
               inputTotalPriceAmmount : priceTotalAmmount
            }
         }
})
