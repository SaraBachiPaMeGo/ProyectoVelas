$(document).ready(function () {
    $('#fragancias').select2({
        placeholder: "Selecciona una fragancia",
        allowClear: true
    });

    $('#pigmentos').select2({
        placeholder: "Selecciona un pigmento",
        allowClear: true
    });

    window.addEventListener('load', function () {
        const pendiente = sessionStorage.getItem('vistaPendiente');
        if (pendiente) {
            const { tipoVista, contenedor, http } = JSON.parse(pendiente);
            sessionStorage.removeItem('vistaPendiente');
            // Ejecuta la carga AJAX automáticamente
            cargarVistaParcial(tipoVista, contenedor, http);
        }
    });
});

function recargarPag(tipoVista, contenedor, http) {
    sessionStorage.setItem('vistaPendiente', JSON.stringify({ tipoVista, contenedor, http }));

    var url = `${window.location.origin}`;
    cargarVistaParcial(tipoVista, contenedor, http);

    window.location.href = url;
}
document.addEventListener('DOMContentLoaded', () => {

    const pendiente = sessionStorage.getItem('vistaPendiente');

    if (pendiente) {
        const { tipoVista, contenedor, http } = JSON.parse(pendiente);
        sessionStorage.removeItem('vistaPendiente');

        ejecutarCargaVista(tipoVista, contenedor, http);
    }
});

function cargarVistaParcial(tipoVista, contenedor, http) {

    sessionStorage.setItem(
        'vistaPendiente',
        JSON.stringify({ tipoVista, contenedor, http })
    );

    // Si NO estamos en Home → redirige SOLO
    if (window.location.pathname !== '/') {
        window.location.href = '/#';
        return; // ⛔ IMPORTANTE
    }

    ejecutarCargaVista(tipoVista, contenedor, http);
}

function ejecutarCargaVista(tipoVista, contenedor, http) {
  
    switch (tipoVista) {
        case 'vela':
            $.ajax({
                url: '/Vela/_'+http+'VelaView', 
                type: 'GET',
                success: function (data) {
                     $('#' + contenedor).html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'velaFin':
            $.ajax({
                url: '/VelaFinalizada/_' + http + 'VelaFinalizadaView',
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data);
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'pedido':
            if (contenedor === '' || contenedor === null) {
                contenedor = 'miContenedor'
            }
            $.ajax({
                url: '/Pedido/_' + http +'PedidoView', 
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'cliente':
            $.ajax({
                url: '/Cliente/_' + http +'ClienteView',
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data);
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'molde':
            $.ajax({
                url: '/Molde/_' + http +'MoldeView', 
                type: 'GET',
                success: function (data) {
                     $('#' + contenedor).html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'frag':
            $.ajax({
                url: '/Fragancia/_' + http +'FragView',
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'pig':
            $.ajax({
                url: '/Pigmento/_' + http +'PigView',
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'mecha':
            $.ajax({
                url: '/Mecha/_' + http + 'MechaView',
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'cera':
            $.ajax({
                url: '/Cera/_' + http +'CeraView',
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'doc':
            $.ajax({
                url: '/Documento/_' + http + 'DocView',
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'end':
            $.ajax({
                url: '/Endurecedor/_' + http + 'EndurecedorView',
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'pack':
            $.ajax({
                url: '/Pack/_' + http + 'PackView',
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        default:
    }
    
}

function manejarCheckbox(contenedor, idCheck, tipoVista, http) {
    const checkbox = document.getElementById(idCheck);

    if (checkbox.checked) {

        document.getElementById(contenedor).style.display = "block";

        // carga la vista parcial de Mecha, Cera, etc
        cargarVistaParcial(tipoVista, contenedor, http);

    } else {
        document.getElementById(contenedor).style.display = "none";
        document.getElementById(contenedor).innerHTML = "";
    }
    //const checkbox = document.getElementById(idCheck);

   
    //if (checkbox.checked) {
    //    //estadoSpan.textContent = "Sí";
    //    if (tipoVista == 'doc') {
    //        //Si la vista es doc, y el check box está seleccionado, mirar a qué estilo pertenece para que aparezca SOLO
    //        //la lista de x estilos (mechas,ceras,...)
    //    }
    //    document.getElementById(contenedor).style.display = "block";
    //    cargarVistaParcial(tipoVista, contenedor, http);
    //} else {
       
    //    console.log(document.getElementById(contenedor))
    //    document.getElementById(contenedor).style.display = "none";
    //}
}
