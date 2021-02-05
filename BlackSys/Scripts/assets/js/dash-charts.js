/*** First Chart in Dashboard page ***/

	$(document).ready(function() {

                $.ajax({
                    url: "http://demo.bdesk.com.mx/Demo/api/DashClient",
                    success: function (data) {

                      //  graficoData(data);
                        cerrado.innerHTML = data.cerrado;
                        abierto.innerHTML = data.abiertos;
                        noc.innerHTML = data.nc;
                        ven.innerHTML = data.vencidos;
                   
                    }
                });

	});
