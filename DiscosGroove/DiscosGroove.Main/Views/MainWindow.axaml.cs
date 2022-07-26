using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using ScottPlot.Avalonia;

namespace DiscosGroove.Main.Views
{
	public partial class MainWindow : Window
	{
		readonly double[] _dataX = new double[100];
		readonly double[] _dataY = new double[100];
		private double _offset = 0;
		private readonly AvaPlot _plot;
		
		public MainWindow()
		{
			InitializeComponent();
			
			_plot = this.Find<AvaPlot>("DemoPlot");
			_plot.Plot.AddScatter(_dataX, _dataY);
			_plot.Plot.SetAxisLimits(0, Math.Tau, -1.1, 1.1);
			_plot.Plot.SetInnerViewLimits(0, Math.Tau, -1.1, 1.1);
			_plot.Plot.SetOuterViewLimits(0, Math.Tau, -1.1, 1.1);
			DispatcherTimer.Run(UpdatePlot, TimeSpan.FromSeconds(0.01));
		}

		private bool UpdatePlot()
		{
			Parallel.For(0, _dataX.Length, i =>
			{
				double x = i * (Math.Tau / _dataX.Length);
				_dataX[i] = x;
				_dataY[i] = Math.Sin(x - _offset);
			});

			_plot.Refresh();
			_offset += 0.01;
			return true;
		}
	}
}