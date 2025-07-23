
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
        default:
    }
    
}

function manejarCheckbox(contenedor, idCheck, tipoVista) {
    const checkbox = document.getElementById(idCheck);

    if (checkbox.checked) {
        //estadoSpan.textContent = "Sí";
        cargarVistaParcial(tipoVista, contenedor);
    }
}
