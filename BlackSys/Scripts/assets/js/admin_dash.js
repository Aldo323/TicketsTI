/*** First Chart in Dashboard page ***/

	$(document).ready(function() {
        var owner = "";
                $.ajax({
                    url: "http://demo.bdesk.com.mx/Demo/api/Graph",
                    success: function (data) {

                      //  graficoData(data);
                          t_a.innerHTML = data.abierto;
                          t_nc.innerHTML = data.nc;
                          t_c.innerHTML = data.cerrado;
                          t_v.innerHTML = data.vencidos;
                    }
                });

                $.ajax({
                    url: "http://demo.bdesk.com.mx/demo/api/My",
                success: function (data) {

                    //  graficoData(data);
                    t_own.innerHTML = data.own;
                    owner = data.own;
                }
        });

        $.ajax({
            url: "http://demo.bdesk.com.mx/demo/api/agentes",
            success: function (data) {
                t_agente.innerHTML = data.contact;
                owner = data.contact;
               
            }
        });


        console.log(owner)
	});
