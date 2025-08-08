$(document).ready(function () {
    $('#fragancias').select2({
        placeholder: "Selecciona una fragancia",
        allowClear: true
    });

    $('#pigmentos').select2({
        placeholder: "Selecciona un pigmento",
        allowClear: true
    });
});

function cargarVistaParcial(tipoVista, contenedor, http) {
    switch (tipoVista) {
        case 'vela':
            $.ajax({
                url: '/Home/_'+http+'VelaView', 
                type: 'GET',
                success: function (data) {
                    $('#miContenedor').html(data); 
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
                url: '/Home/_' + http +'PedidoView', 
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
                url: '/Home/_' + http +'ClienteView',
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
                url: '/Home/_' + http +'MoldeView', 
                type: 'GET',
                success: function (data) {
                    $('#miContenedor').html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'frag':
            $.ajax({
                url: '/Home/_' + http +'FragView',
                type: 'GET',
                success: function (data) {
                    $('#miContenedor').html(data);
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'pig':
            $.ajax({
                url: '/Home/_' + http +'PigView',
                type: 'GET',
                success: function (data) {
                    $('#miContenedor').html(data);
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'mecha':
            $.ajax({
                url: '/Home/_' + http +'MechaView',
                type: 'GET',
                success: function (data) {
                    $('#miContenedor').html(data);
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'cera':
            $.ajax({
                url: '/Home/_' + http +'CeraView',
                type: 'GET',
                success: function (data) {
                    $('#miContenedor').html(data);
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'end':
            $.ajax({
                url: '/Home/_' + http + 'EndurecedorView',
                type: 'GET',
                success: function (data) {
                    $('#miContenedor').html(data);
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'pack':
            $.ajax({
                url: '/Home/_' + http + 'PackView',
                type: 'GET',
                success: function (data) {
                    $('#miContenedor').html(data);
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
        //estadoSpan.textContent = "Sí";
        document.getElementById(contenedor).style.display = "block";
        cargarVistaParcial(tipoVista, contenedor, http);
    } else {
        console.log(document.getElementById(contenedor))
        document.getElementById(contenedor).style.display = "none";
    }
}
