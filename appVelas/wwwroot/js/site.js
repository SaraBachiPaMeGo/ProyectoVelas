const rutasDelete = {
    Vela: '/Vela/Delete/',
    VelaFinalizada: '/VelaFinalizada/Delete/',
    Pedido: '/Pedido/Delete/',
    Molde: '/Molde/Delete/',
    Fragancia: '/Fragancia/Delete/',
    Pigmento: '/Pigmento/Delete/',
    Mecha: '/Mecha/Delete/',
    Cera: '/Cera/Delete/',
    Documento: '/Documento/Delete/',
    Endurecedor: '/Endurecedor/Delete/',
    Pack: '/Pack/Delete/'
};
let archivoSeleccionado = null;

document.addEventListener('DOMContentLoaded', () => {

    const pendiente = sessionStorage.getItem('vistaPendiente');

    if (pendiente) {
        const { tipoVista, contenedor, http } = JSON.parse(pendiente);
        sessionStorage.removeItem('vistaPendiente');

        ejecutarCargaVista(tipoVista, contenedor, http);
    }

    let confirmCallback = null;

    const modal = document.getElementById("confirmModal");
    const title = document.getElementById("modalTitle");
    const message = document.getElementById("modalMessage");
    const btnConfirm = document.getElementById("modalConfirm");
    const btnCancel = document.getElementById("modalCancel");

    document.getElementById("formMolde").addEventListener("submit", function (e) {
        e.preventDefault();

        const formData = new FormData(this);

        if (archivoSeleccionado) {
            formData.set("file", archivoSeleccionado);
        }

        fetch(this.action, {
            method: "POST",
            body: formData
        })
            .then(r => r.text())
            .then(html => {
                document.body.innerHTML = html; // o lo que necesites
            })
            .catch(err => console.error(err));
    });

    document.getElementById("modalCancel").onclick = () => {
        cerrarModal();
    };

    function cerrarModal() { modal.classList.add("hidden"); confirmCallback = null; }

    document.getElementById("modalConfirm").onclick = () => {
        if (confirmCallback) confirmCallback();
        cerrarModal();
    };
        
    if (!modal || !btnConfirm || !btnCancel) {
        console.error("Modal no encontrado en el DOM");
        return;
    }

    window.mostrarConfirmacion = function (titulo, texto, callback) {
        title.innerText = titulo;
        message.innerText = texto;
        confirmCallback = callback;
        modal.classList.remove("hidden");
    };

    btnCancel.addEventListener("click", cerrarModal);

    btnConfirm.addEventListener("click", function () {
        if (confirmCallback) confirmCallback();
        cerrarModal();
    });        
})

function cerrarModal() {
    modal.classList.add("hidden");
    confirmCallback = null;
}

function mostrarConfirmacion(titulo, mensaje, onConfirm) {
    confirmCallback = onConfirm;

    document.getElementById("modalTitle").innerText = titulo;
    document.getElementById("modalMessage").innerText = mensaje;
    document.getElementById("confirmModal").classList.remove("hidden");
}

$(document).on('click', '.btn-eliminar', function () {
    const id = $(this).data('id');
    const tipoVista = $(this).data('tipo');

    mostrarConfirmacion(
        "Eliminar registro",
        "¿Seguro que deseas eliminar este registro?",
        () => eliminarGenerico(id, tipoVista)
    );
});

function eliminarGenerico(id, tipoVista) {

    const url = rutasDelete[tipoVista];
    if (!url) {
        alert('Tipo de vista no soportado');
    } else {
        $.ajax({
            url: url + id,
            type: 'POST',
            success: function () {

                const fila = document.getElementById(`eliminar-row-${id}`);
                if (fila) {
                    fila.classList.add("fade-out");
                    setTimeout(() => fila.remove(), 300);
                }
            },
            error: function () {
                alert('Error al eliminar el registro.');
            }
        });
    }    
}

$(document).on('keyup', '#buscadorTabla', function () {

    const texto = $(this).val().toLowerCase().trim();
    const filas = $('#tablaDatos tr');

    let coincidencias = [];

    filas.each(function () {
        const fila = $(this);

        //Buscar por nombre
        const contenido = fila.find('.col-buscar').text().toLowerCase();


        if (contenido.includes(texto)) {
            coincidencias.push(fila);
            fila.show();
        } else {
            fila.hide();
        }
    });

    // 🔝 Reordenar: coincidencias arriba
    coincidencias.forEach(fila => {
        $('#tablaDatos').prepend(fila);
    });
});

function initDropzone() {
    const dropzone = document.getElementById("dropzone");
    const fileInput = document.getElementById("fileInput");
    const preview = document.getElementById("preview");
    const status = document.getElementById("status");

    if (!dropzone) return; // 🔥 evita errores en otras vistas

    // listeners aquí
    dropzone.addEventListener("click", () => fileInput.click());

    ["dragenter", "dragover", "dragleave", "drop"].forEach(eventName => {
        dropzone.addEventListener(eventName, e => {
            e.preventDefault();
            e.stopPropagation();
        });
    });

    dropzone.addEventListener("dragover", () => {
        dropzone.classList.add("dragover");
    });

    dropzone.addEventListener("dragleave", () => {
        dropzone.classList.remove("dragover");
    });

    dropzone.addEventListener("drop", (e) => {
        dropzone.classList.remove("dragover");

        const file = e.dataTransfer.files[0];
        if (!file) return;

        archivoSeleccionado = file;

        // 🔥 CLAVE
        const dataTransfer = new DataTransfer();
        dataTransfer.items.add(file);
        fileInput.files = dataTransfer.files;
        mostrarPreview(file);
    });

    function mostrarPreview(file) {
            const reader = new FileReader();
            reader.onload = () => {
                preview.src = reader.result;
                preview.style.display = "block";
            };
            reader.readAsDataURL(file);
        }
}
   
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
                    initDropzone();
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


