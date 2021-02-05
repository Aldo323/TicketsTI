
var handleSelect2 = function () {
    var e = function () {
        function e(e) {
            if (e.loading) return e.text;
            var t = "<div class='select2-result-repository clearfix'><div class='select2-result-repository__avatar'><img src='" + e.owner.avatar_url + "' /></div><div class='select2-result-repository__meta'><div class='select2-result-repository__title'>" + e.full_name + "</div>"; return e.description && (t += "<div class='select2-result-repository__description'>" + e.description + "</div>"), t += "<div class='select2-result-repository__statistics'><div class='select2-result-repository__forks'><span class='glyphicon glyphicon-flash'></span> " + e.forks_count + " Forks</div><div class='select2-result-repository__stargazers'><span class='glyphicon glyphicon-star'></span> " + e.stargazers_count + " Stars</div><div class='select2-result-repository__watchers'><span class='glyphicon glyphicon-eye-open'></span> " + e.watchers_count + " Watchers</div></div></div></div>"
        }
        function t(e) {
            return e.full_name || e.text
        }
        $.fn.select2.defaults.set("theme", "bootstrap");
        var s = "Select a State";
        $('.select3').select2();
        $(".select2, .select2-multiple").select2({ placeholder: s, width: null }),
        $(".select2-allow-clear").select2({ allowClear: !0, placeholder: s, width: null }), $(".js-data-example-ajax").select2({ width: "off", ajax: { url: "https://api.github.com/search/repositories", dataType: "json", delay: 250, data: function (e) { return { q: e.term, page: e.page } }, processResults: function (e, t) { return { results: e.items } }, cache: !0 }, escapeMarkup: function (e) { return e }, minimumInputLength: 1, templateResult: e, templateSelection: t }), $("button[data-select2-open]").click(function () { $("#" + $(this).data("select2-open")).select2("open") }), $(":checkbox").on("click", function () { $(this).parent().nextAll("select").prop("disabled", !this.checked) }), $(".select2, .select2-multiple, .select2-allow-clear, .js-data-example-ajax").on("select2:open", function () { if ($(this).parents("[class*='has-']").length) for (var e = $(this).parents("[class*='has-']")[0].className.split(/\s+/), t = 0; t < e.length; ++t) e[t].match("has-") && $("body > .select2-container").addClass(e[t]) }), $(".js-btn-set-scaling-classes").on("click", function () { $("#select2-multiple-input-sm, #select2-single-input-sm").next(".select2-container--bootstrap").addClass("input-sm"), $("#select2-multiple-input-lg, #select2-single-input-lg").next(".select2-container--bootstrap").addClass("input-lg"), $(this).removeClass("btn-primary btn-outline").prop("disabled", !0) })
    }; return { init: function () { e() } }
}();

var handleTables = function () {
    var initTables = function () {
        var table = $('#sys-table');
        var oTable = table.dataTable({
            "language": {
                "aria": {
                    "sortAscending": ": activate to sort column ascending",
                    "sortDescending": ": activate to sort column descending"
                },
                "emptyTable": "No data available in table",
                "info": "Showing _START_ to _END_ of _TOTAL_ entries",
                "infoEmpty": "No entries found",
                "infoFiltered": "(filtered1 from _MAX_ total entries)",
                "lengthMenu": "_MENU_ entries",
                "search": "Filter:",
                "zeroRecords": "No matching records found"
            },

            buttons: [
                //{ extend: 'print', className: 'btn dark btn-outline' },
                //{ extend: 'pdf', className: 'btn green btn-outline' },
                //{ extend: 'csv', className: 'btn purple btn-outline ' }
            ],
            responsive: {
                details: {

                }
            },

            "order": [
                [0, 'asc']
            ],

            "lengthMenu": [
                [5, 10, 15, 20, -1],
                [5, 10, 15, 20, "All"]
            ],
            "pageLength": 10,

            "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
        });
        $('.dataTables_length select').addClass("select2");
    };
    
    return {
        init: function () {
            if (!jQuery().dataTable) {
                return;
            }

            initTables();                
        }

    };
}();

var UIModals = function () {

    var handleModals = function () {
        $(".modalx").draggable({
            handle: ".modal-header"
        });
    }

    return {
        init: function () {
            handleModals();
        }

    };

}();

function playAudio(file) {
    if (file === 'alert')
        document.getElementById('audio-alert').play();

    if (file === 'fail')
        document.getElementById('audio-fail').play();
}

function showNotification(msg, msgType) {
    if (msg.length > 3) {       
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-bottom-right",
            "onclick": null,
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "10000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }        
        if (msgType === 'S') {
            playAudio('alert');
            toastr.success(msg, "Operacion Exitosa")
        }
        else if (msgType === 'E') {
            playAudio('fail');
            toastr.error(msg, "Error!")
        }
        else if (msgType === 'I') {
            playAudio('fail');
            toastr.info(msg, "Informacion!")
        }
        else if (msgType === 'W') {
            playAudio('alert');
            toastr.warning(msg, "Alerta!")
        }
        else {
            playAudio('fail');
            toastr.error(msg, "Error Desconocido")
        }
    }
}

function showDialog(controller, action, formType)
{
    var url = "/" + controller + "/" + action;
    $('#modal-content').load(url, function () {
        $('#modal-container').modal({ keyboard: true }, 'show');        
    });

}

function showEditDialog(controller, action, itemID) {
    var url = "/" + controller + "/" + action+"/"+itemID;
    $('#modal-content').load(url, function () {
        $('#modal-container').modal({ keyboard: true }, 'show');
    });

}


jQuery(document).ready(function () {
    handleSelect2.init();
    handleTables.init();
    UIModals.init();    
   
});



