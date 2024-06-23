using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class OrdersPredictionService : IOrdersPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly IOrderRepository _orderRepository;

        public OrdersPredictionService(IOrderRepository orderRepository)
        {
            _mlContext = new MLContext();
            _orderRepository = orderRepository;
            _model = TrainModel();
        }


        public double PredictOrders(int productId, int month, int year)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<DataOrder, OrdersPrediction>(_model);

            var SalesData = new DataOrder
            {
                ProductId = productId,
                Month = month,
                Year = year
            };

            var predictor = predictionEngine.Predict(SalesData);
            return predictor.PredictedSales;
        }

        private ITransformer TrainModel()
        {
            var data = _orderRepository.GetAll().Select(order => new DataOrder
            {
                ProductId = order.ProductId,
                Month = order.Date.Month,
                Year = order.Date.Year,
                Sales = order.Quantity
            }).ToList();

            var trainingData = _mlContext.Data.LoadFromEnumerable(data);

            var pipeline = _mlContext.Transforms.Concatenate("Features", "ProductId", "Month", "Year")
                .Append(_mlContext.Transforms.CopyColumns("Label", "Sales"))
                .Append(_mlContext.Regression.Trainers.Sdca());

            var model = pipeline.Fit(trainingData);
            return model;
        }
    }
    public class OrdersPrediction
    {
        [ColumnName("Score")]
        public float PredictedSales { get; set; }    
    }
    public class DataOrder
    {
        public int ProductId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Sales { get; set; }
    }
}
