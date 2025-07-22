
function cargarVistaParcial(tipoVista, contenedor, http) {
    switch (tipoVista) {
        case 'vela':
            $.ajax({
                url: '/Home/_'+http+'VelaView', // Reemplaza con la URL correcta
                type: 'GET',
                success: function (data) {
                    $('#miContenedor').html(data); // Reemplaza #miContenedor con el ID del elemento donde insertar la vista
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
                url: '/Home/_CrearPedidoView', 
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
                url: '/Home/_CrearClienteView',
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
                url: '/Home/_CrearMoldeView', 
                type: 'GET',
                success: function (data) {
                    $('#miContenedor').html(data); 
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                }
            });
            break;
        case 'detalleVela':
            $.ajax({
                url: '/Home/_DetallesVelaView',
                type: 'GET',
                success: function (data) {
                    $('#' + contenedor).html(data);
                },
                error: function () {
                    alert('Error al cargar la vista parcial.');
                    console.log(error)
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
