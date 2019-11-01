using Model.ViewModels.Invoice;
using Model.ViewModels.Order;
using Model.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Serialization;

namespace BLL
{
    class ShopProductInfo
    {
        public int Id;
        public string Name;
        public decimal Price;
    }
    public class BLShopCart
    {
        readonly Dictionary<string, ShopProductInfo> ProductDictionary = new Dictionary<string, ShopProductInfo>()
        {
            {"1First",new ShopProductInfo{Id = 1392, Name = "Early First", Price = 871.25m}},
            {"1Extra",new ShopProductInfo{Id = 1393, Name = "Early Extra", Price = 666.25m } },
            {"2First",new ShopProductInfo{Id = 1394 ,Name = "Regular First", Price = 973.75m } },
            {"2Extra",new ShopProductInfo{Id = 1395 ,Name = "Regular Extra", Price = 768.75m } },
            {"3First",new ShopProductInfo{Id = 1396 ,Name = "Late First", Price = 1127.50m } },
            {"3Extra",new ShopProductInfo{Id = 1397 ,Name = "Late Extra", Price = 973.75m } },
            {"ExtraParticipant",new ShopProductInfo{Id = 1398 ,Name = "Extra Participant", Price = 153.75m } }
        };

        HttpClient client = new HttpClient();

        public readonly string ServiceKey = "ff23964b7b3ccf746f914c4adedb76e7";
        public readonly string BaseServiceUrl = "https://shopcart.nmsu.edu/service/95/";

        #region Handel Checkout
        public Model.ShopCart.Checkout.Result HandelCheckout(List<VmTeamSelection> teamSelectionList, string advisorUserId)
        {
            var productCode = 0;

            var blInvoice = new BLInvoice();

            var invoiceList = blInvoice.GetInvoiceFullInfoByUserId(advisorUserId, false);

            var newShopOrder = CreateOrder();

            if (newShopOrder.Error == "0")
            {
                var blProduct = new BLProduct();
                var productList = new List<VmProduct>();

                var blOrder = new BLOrder();

                var newOrder = new VmOrder()
                {
                    InvoiceId = invoiceList.Id,
                    OrderDate = DateTime.Now,
                    ShopOrderId = int.Parse(newShopOrder.Order.Id),
                    UserId = advisorUserId,
                    Complete = false
                };

                blOrder.CreateOrder(newOrder);

                foreach (var item in invoiceList.InvoiceDetails)
                {
                    if (item.IsChecked)
                    {
                        productCode = ProductDictionary[item.PaymentRuleId + item.FirstTeamOrExtraTeam].Id;
                        productList.Add(new VmProduct
                        {
                            Amount = 1,
                            ShopOrderId = int.Parse(newShopOrder.Order.Id),
                            ShopProductId = productCode

                        });

                        var orderInfo = AddProduct(newShopOrder.Order.Id, productCode.ToString());
                        if (orderInfo.Error != "0")
                        {
                            return null;
                        }

                        if (item.ExtraParticipantCount > 0)
                        {
                            productCode = ProductDictionary["ExtraParticipant"].Id;

                            orderInfo = AddProduct(newShopOrder.Order.Id, productCode.ToString(), item.ExtraParticipantCount);

                            if (orderInfo.Error != "0")
                            {
                                return null;
                            }
                            else
                            {
                                productList.Add(new VmProduct
                                {
                                    Amount = item.ExtraParticipantCount,
                                    ShopOrderId = int.Parse(newShopOrder.Order.Id),
                                    ShopProductId = productCode

                                });

                            }
                        }

                    }
                }

                blProduct.CreateBatchProduct(productList);

                return PrepareCheckout(int.Parse(newShopOrder.Order.Id));

            }

            return null;

        }
        public Model.ShopCart.Checkout.Result HandelCheckout(int invoiceId, string advisorUserId)
        {
            var productCode = 0;

            var blInvoice = new BLInvoice();

            var invoiceList = blInvoice.GetExtraMemberInvoiceFullInfoByUserId(advisorUserId);

            var newShopOrder = CreateOrder();

            if (newShopOrder.Error == "0")
            {
                var blProduct = new BLProduct();
                var productList = new List<VmProduct>();

                var blOrder = new BLOrder();

                var newOrder = new VmOrder()
                {
                    InvoiceId = invoiceList.Id,
                    OrderDate = DateTime.Now,
                    ShopOrderId = int.Parse(newShopOrder.Order.Id),
                    UserId = advisorUserId,
                    Complete = false
                };

                blOrder.CreateOrder(newOrder);

                foreach (var item in invoiceList.InvoiceDetails)
                {
                    if (item.IsChecked)
                    {
                        productCode = ProductDictionary[item.PaymentRuleId + item.FirstTeamOrExtraTeam].Id;
                        productList.Add(new VmProduct
                        {
                            Amount = 1,
                            ShopOrderId = int.Parse(newShopOrder.Order.Id),
                            ShopProductId = productCode

                        });

                        var orderInfo = AddProduct(newShopOrder.Order.Id, productCode.ToString());
                        if (orderInfo.Error != "0")
                        {
                            return null;
                        }

                        if (item.ExtraParticipantCount > 0)
                        {
                            productCode = ProductDictionary["ExtraParticipant"].Id;

                            orderInfo = AddProduct(newShopOrder.Order.Id, productCode.ToString(), item.ExtraParticipantCount);

                            if (orderInfo.Error != "0")
                            {
                                return null;
                            }
                            else
                            {
                                productList.Add(new VmProduct
                                {
                                    Amount = item.ExtraParticipantCount,
                                    ShopOrderId = int.Parse(newShopOrder.Order.Id),
                                    ShopProductId = productCode

                                });

                            }
                        }

                    }
                }

                blProduct.CreateBatchProduct(productList);

                return PrepareCheckout(int.Parse(newShopOrder.Order.Id));

            }

            return null;

        }

        public Model.ShopCart.Checkout.Result HandelCheckoutExtraMember(int invoiceId, string advisorUserId)
        {
            var productCode = 0;

            var blInvoice = new BLInvoice();

            var invoiceList = blInvoice.GetExtraMemberInvoiceFullInfoByUserId(advisorUserId);

            var newShopOrder = CreateOrder();

            if (newShopOrder.Error == "0")
            {
                var blProduct = new BLProduct();
                var productList = new List<VmProduct>();

                var blOrder = new BLOrder();

                var newOrder = new VmOrder()
                {
                    InvoiceId = invoiceList.Id,
                    OrderDate = DateTime.Now,
                    ShopOrderId = int.Parse(newShopOrder.Order.Id),
                    UserId = advisorUserId,
                    Complete = false
                };

                blOrder.CreateOrder(newOrder);

                foreach (var item in invoiceList.InvoiceDetails)
                {
                    if (item.IsChecked)
                    {

                        if (item.ExtraParticipantCount > 0)
                        {
                            productCode = ProductDictionary["ExtraParticipant"].Id;

                            var orderInfo = AddProduct(newShopOrder.Order.Id, productCode.ToString(), item.ExtraParticipantCount);

                            if (orderInfo.Error != "0")
                            {
                                return null;
                            }
                            else
                            {
                                productList.Add(new VmProduct
                                {
                                    Amount = item.ExtraParticipantCount,
                                    ShopOrderId = int.Parse(newShopOrder.Order.Id),
                                    ShopProductId = productCode

                                });

                            }
                        }

                    }
                }

                blProduct.CreateBatchProduct(productList);

                return PrepareCheckout(int.Parse(newShopOrder.Order.Id));

            }

            return null;

        }

        /// <summary>
        /// url = ///service/[shopid]/orders/[orderid]/checkout
        /// </summary>
        /// <returns></returns>
        public Model.ShopCart.Checkout.Result PrepareCheckout(int orderId)
        {
            var checkoutResult = new Model.ShopCart.Checkout.Result();
            try
            {

                var url = BaseServiceUrl + "orders/" + orderId + "/checkout?key=" + ServiceKey;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var ser = new XmlSerializer(typeof(Model.ShopCart.Checkout.Result));
                    checkoutResult = (Model.ShopCart.Checkout.Result)ser.Deserialize(response.Content.ReadAsStreamAsync().Result);
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return checkoutResult;
        }
        #endregion Handel Checkout

        #region Order

        /// <summary>
        /// url = service/[shopid]/orders/create
        /// </summary>
        /// <returns></returns>
        public Model.ShopCart.Order.NewOrder.Result CreateOrder()
        {
            var newOrder = new Model.ShopCart.Order.NewOrder.Result();
            try
            {

                var url = BaseServiceUrl + "orders/create?key=" + ServiceKey;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    var ser = new XmlSerializer(typeof(Model.ShopCart.Order.NewOrder.Result));
                    newOrder = (Model.ShopCart.Order.NewOrder.Result)ser.Deserialize(response.Content.ReadAsStreamAsync().Result);
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return newOrder;
        }

        /// <summary>
        /// url = /service/[shopid]/orders
        /// </summary>
        /// <returns></returns>
        public Model.ShopCart.Order.AllOrders.Result GetAllOrders()
        {
            var allOrders = new Model.ShopCart.Order.AllOrders.Result();
            try
            {

                var url = BaseServiceUrl + "orders?key=" + ServiceKey;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    var ser = new XmlSerializer(typeof(Model.ShopCart.Order.AllOrders.Result));
                    allOrders = (Model.ShopCart.Order.AllOrders.Result)ser.Deserialize(response.Content.ReadAsStreamAsync().Result);
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return allOrders;
        }

        /// <summary>
        /// url = /service/[shopid]/orders?verbose=1
        /// </summary>
        /// <returns></returns>
        public Model.ShopCart.Order.AllOrdersVerbose.Result GetAllOrdersVerbose()
        {
            var allOrdersVerbose = new Model.ShopCart.Order.AllOrdersVerbose.Result();
            try
            {

                var url = BaseServiceUrl + "orders?key=" + ServiceKey + "&verbose=1";
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    var ser = new XmlSerializer(typeof(Model.ShopCart.Order.AllOrdersVerbose.Result));
                    allOrdersVerbose = (Model.ShopCart.Order.AllOrdersVerbose.Result)ser.Deserialize(response.Content.ReadAsStreamAsync().Result);
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return allOrdersVerbose;
        }

        /// <summary>
        /// url = /service/[shopid]/orders/[orderid]
        /// </summary>
        /// <returns></returns>
        public Model.ShopCart.Order.OrderInfo.Result GetOrderInfo(int orderId)
        {
            var orderInfo = new Model.ShopCart.Order.OrderInfo.Result();
            try
            {

                var url = BaseServiceUrl + "orders/" + orderId + "?key=" + ServiceKey;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    var ser = new XmlSerializer(typeof(Model.ShopCart.Order.OrderInfo.Result));
                    orderInfo = (Model.ShopCart.Order.OrderInfo.Result)ser.Deserialize(response.Content.ReadAsStreamAsync().Result);
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return orderInfo;
        }

        /// <summary>
        /// url = /service/[shopid]/orders/[orderid]?verbose=1
        /// </summary>
        /// <returns></returns>
        public Model.ShopCart.Order.OrderInfoVerbose.Result GetOrderInfoVerbose(string orderId)
        {
            var orderInfoInfoVerbose = new Model.ShopCart.Order.OrderInfoVerbose.Result();
            try
            {

                var url = BaseServiceUrl + "orders/" + orderId + "?key=" + ServiceKey + "&verbose=1";
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    var ser = new XmlSerializer(typeof(Model.ShopCart.Order.OrderInfoVerbose.Result));
                    orderInfoInfoVerbose = (Model.ShopCart.Order.OrderInfoVerbose.Result)ser.Deserialize(response.Content.ReadAsStreamAsync().Result);
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return orderInfoInfoVerbose;
        }

        public Model.ShopCart.Order.OrderInfo.Result GetCheckoutStatus(string userId, int invoiceId, out int? lastOrderId)
        {
            var blOrder = new BLOrder();
            lastOrderId = blOrder.GetLastOrder(userId, invoiceId);

            if (lastOrderId == null)
            {
                return null;
            }

            var lastOrderInfo = GetOrderInfo(lastOrderId.Value);


            if (lastOrderInfo.Order != null && (lastOrderInfo.Order.Status.ToLower() == "confirmed" || lastOrderInfo.Order.Status.ToLower() == "fulfilled"))
            {
                return lastOrderInfo;
            }

            return null;
        }

        public Model.ShopCart.Order.OrderInfo.Result GetCheckoutStatus(int invoiceId, out int? lastOrderId)
        {
            var blOrder = new BLOrder();
            lastOrderId = blOrder.GetLastOrder(invoiceId);

            if (lastOrderId == null)
            {
                return null;
            }

            var lastOrderInfo = GetOrderInfo(lastOrderId.Value);


            if (lastOrderInfo.Order != null && (lastOrderInfo.Order.Status.ToLower() == "confirmed" || lastOrderInfo.Order.Status.ToLower() == "fulfilled"))
            {
                return lastOrderInfo;
            }

            return null;
        }

        #endregion Order

        #region Product

        /// <summary>
        /// url = /service/[shopid]/orders/[orderid]/add/[productid]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.ShopCart.Order.OrderInfoVerbose.Result AddProduct(string orderId, string productId, int amount = 1)
        {
            var orderInfoInfoVerbose = new Model.ShopCart.Order.OrderInfoVerbose.Result();

            try
            {
                var url = BaseServiceUrl + "orders/" + orderId + "/add/" + productId + "?key=" + ServiceKey + "&amount=" + amount;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    var ser = new XmlSerializer(typeof(Model.ShopCart.Order.OrderInfoVerbose.Result));
                    orderInfoInfoVerbose = (Model.ShopCart.Order.OrderInfoVerbose.Result)ser.Deserialize(response.Content.ReadAsStreamAsync().Result);
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return orderInfoInfoVerbose;
        }
        public Model.ShopCart.Product.Result GetProductList(int orderId)
        {
            Model.ShopCart.Product.Result productList = null;

            try
            {
                var url = BaseServiceUrl + "order/" + orderId + "?key=" + ServiceKey + "&verbose=1";
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    var ser = new XmlSerializer(typeof(Model.ShopCart.Product.Result));
                    productList = (Model.ShopCart.Product.Result)ser.Deserialize(response.Content.ReadAsStreamAsync().Result);
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return productList;
        }

        #endregion Product



    }
}
