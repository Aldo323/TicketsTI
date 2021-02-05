/*** First Chart in Dashboard page ***/

	$(document).ready(function() {

                $.ajax({
                    url: "api/Graph",
                    success: function (data) {

                      //  graficoData(data);
                          t_a.innerHTML = data.abierto;
                          t_nc.innerHTML = data.nc;
                          t_c.innerHTML = data.cerrado;

                    }
                });

                $.ajax({
                    url: "api/My",
                success: function (data) {

                    //  graficoData(data);
                    t_own.innerHTML = data.own;

                }
            });
 function graficoData(data) {

                  
                   // t_nc.innerHTML = data.nc;
                    info = new Highcharts.Chart({
                        chart: {
                            renderTo: 'space',
                            margin: [0, 0, 0, 0],
                            backgroundColor: null,
                            plotBackgroundColor: 'none',

                        },

                        title: {
                            text: null
                        },

                        tooltip: {
                            formatter: function () {
                                return this.point.name + ': ' + this.y ;

                            }
                        },
                        series: [
                            {
                                borderWidth: 2,
                                borderColor: '#F1F3EB',
                                shadow: false,
                                type: 'pie',
                                name: 'Income',
                                innerSize: '65%',
                                data: [
                                    { name: 'Abiertos', y: data.abierto, color: '#b2c831' },
                                    { name: 'Sin Categorizar', y: data.nc, color: '#3d3d3d' }
                                ],
                                dataLabels: {
                                    enabled: false,
                                    color: '#000000',
                                    connectorColor: '#000000'
                                }
                            }]
                    });


                     info = new Highcharts.Chart({
                        chart: {
                            renderTo: 'load',
                            margin: [0, 0, 0, 0],
                            backgroundColor: null,
                            plotBackgroundColor: 'none',

                        },

                        title: {
                            text: null
                        },

                        tooltip: {
                            formatter: function () {
                                return this.point.name + ': ' + this.y;

                            }
                        },
                        series: [
                            {
                                borderWidth: 2,
                                borderColor: '#F1F3EB',
                                shadow: false,
                                type: 'pie',
                                name: 'Income',
                                innerSize: '65%',
                                data: [
                                    
                                    { name: 'Sin Categorizar', y: data.nc, color: '#fa1d2d' },
                                    { name: 'Abiertos', y:data.abierto , color: '#3d3d3d' }
                                ],
                                dataLabels: {
                                    enabled: false,
                                    color: '#000000',
                                    connectorColor: '#000000'
                                }
                            }]
                    });

                      info = new Highcharts.Chart({
                        chart: {
                            renderTo: 'cerrados',
                            margin: [0, 0, 0, 0],
                            backgroundColor: null,
                            plotBackgroundColor: 'none',

                        },

                        title: {
                            text: null
                        },

                        tooltip: {
                            formatter: function () {
                                return this.point.name + ': ' + this.y;

                            }
                        },
                        series: [
                            {
                                borderWidth: 2,
                                borderColor: '#F1F3EB',
                                shadow: false,
                                type: 'pie',
                                name: 'Income',
                                innerSize: '65%',
                                data: [
                                    
                                    { name: 'Cerrados', y: data.cerrado, color: '#4fc9ee' },
                                    { name: 'Abiertos', y:data.abierto , color: '#3d3d3d' }
                                ],
                                dataLabels: {
                                    enabled: false,
                                    color: '#000000',
                                    connectorColor: '#000000'
                                }
                            }]
                    });
}
		
	});
