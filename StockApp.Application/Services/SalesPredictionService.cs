using Microsoft.ML;
using Microsoft.ML.Data;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class SalesPredictionService : ISalesPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly IOrderRepository  _orderRepository;

        public SalesPredictionService(IOrderRepository orderRepository)
        {
             _mlContext = new MLContext();   
            _orderRepository = orderRepository;
            _model = TrainModel();
        }


        public double PredictOrders(int productId, int month, int year)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SalesDTO,SalesPrediction>(_model);

            var SalesData = new SalesDTO
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
            var data = _orderRepository.GetAll().Select(order=> new SalesDTO
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
    public class SalesPrediction
    {
        [ColumnName("Score")]
        public float PredictedSales { get; set; }
    }
}
