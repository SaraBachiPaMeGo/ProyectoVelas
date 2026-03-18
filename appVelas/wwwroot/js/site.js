const rutasDelete = {
    Vela: '/Vela/Delete/',
    VelaFinalizada: '/VelaFinalizada/Delete/',
    Pedido: '/Pedido/Delete/',
    Molde: '/Molde/Delete/',
    Inventario: '/Inventario/Delete/',
    Fragancia: '/Fragancia/Delete/',
    Pigmento: '/Pigmento/Delete/',
    Mecha: '/Mecha/Delete/',
    Cera: '/Cera/Delete/',
    Documento: '/Documento/Delete/',
    Endurecedor: '/Endurecedor/Delete/',
    Pack: '/Pack/Delete/'
};

let archivoSeleccionado = null;

let filaAEliminar = null;

window.inicializarVelaForm = function () {

    configurarModulo(
        "fraganciasList",
        "fraganciasEditor",
        "fraganciasCarrito",
        "vfrag",
        "IDFrag"
    );

    configurarModulo(
        "pigmentosList",
        "pigmentosEditor",
        "pigmentosCarrito",
        "vpig",
        "IDPig"
    );
};

document.addEventListener('DOMContentLoaded', () => {

    window.inicializarVelaForm?.();

    if (document.getElementById("fraganciasList")) {
        configurarModulo(
            "fraganciasList",
            "fraganciasEditor",
            "fraganciasCarrito",
            "vfrag",
            "IDFrag"
        );
    }

    if (document.getElementById("pigmentosList")) {
        configurarModulo(
            "pigmentosList",
            "pigmentosEditor",
            "pigmentosCarrito",
            "vpig",
            "IDPig"
        );
    }

    const pendiente = sessionStorage.getItem('vistaPendiente');

    initDropzone();

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
       
    if (document.getElementById("formMolde") !== null) {
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
    }

    $(document).ready(function () {
        $('.select2').select2({
            placeholder: "Selecciona",
            width: '100%'
        });
    });
           
    if (!modal || !btnConfirm || !btnCancel) {
        console.error("Modal no encontrado en el DOM");
        return;
    }

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

    btnCancel.addEventListener("click", cerrarModal);

    btnConfirm.addEventListener("click", function () {
        if (confirmCallback) confirmCallback();
        cerrarModal();
    });    

    $(document).on('click', '.btn-eliminar', function () {
        const id = $(this).data('id');
        const tipoVista = $(this).data('tipo');

        // 🔥 Guardar la fila
        filaAEliminar = $(this).closest("tr")[0];

        mostrarConfirmacion(
            "Eliminar registro",
            "¿Seguro que deseas eliminar este registro?",
            () => eliminarGenerico(id, tipoVista)
        );
    });


   
    
})

function configurarModulo(listId, editorId, carritoId, modelName, idProperty) {

    const list = document.getElementById(listId);

    if (!list) {
        console.warn("No existe:", listId);
        return;
    }

    const editor = document.getElementById(editorId);
    const carrito = document.getElementById(carritoId);

    if (!editor || !carrito) {
        console.warn("Editor o carrito no encontrados");
        return;
    }

    let itemsAñadidos = [];

    // CLICK EN ELEMENTO DE LISTA
    list.addEventListener("click", function (e) {

        if (!e.target.classList.contains("selectable-item"))
            return;

        const id = e.target.dataset.id;
        const name = e.target.dataset.name;

        mostrarEditor(id, name);
    });


    function mostrarEditor(id, name) {

        editor.innerHTML = `
            <div class="card p-3 mb-2">
                <h6>${name}</h6>

                <label asp-for="Cantidad">Cantidad</label>
                <input type="number"
                       class="form-control mb-2 cantidadInput"
                       min="1" />

                <i class="fa-solid fa-circle-plus fa-xl text-success"
                   style="cursor:pointer"></i>
            </div>
        `;

        const btnAdd = editor.querySelector(".fa-circle-plus");
        const inputCantidad = editor.querySelector(".cantidadInput");

        btnAdd.addEventListener("click", function () {

            const cantidad = inputCantidad.value;

            if (!cantidad) {
                alert("Introduce cantidad");
                return;
            }

            if (itemsAñadidos.some(x => x.id === id)) {
                alert("Ya añadido");
                return;
            }

            itemsAñadidos.push({ id, name, cantidad });

            renderCarrito();
            editor.innerHTML = "";
        });
    }


    function renderCarrito() {

        carrito.innerHTML = "";

        itemsAñadidos.forEach((item, index) => {

            const div = document.createElement("div");
            div.classList.add("card", "p-2", "mb-2");

            div.innerHTML = `
                <div class="d-flex justify-content-between align-items-center">
                    <span><strong>${item.name}</strong> - ${item.cantidad}</span>
                    <i class="fa-solid fa-xmark text-danger"
                       style="cursor:pointer"></i>
                </div>

                <input type="hidden"
                       name="${modelName}[${index}].${idProperty}"
                       value="${item.id}" />
                <input type="hidden"
                       name="${modelName}[${index}].ColorNombre"
                       value="${item.name}" />
                <input type="hidden"
                       name="${modelName}[${index}].Cantidad"
                       value="${item.cantidad}" />
            `;

            const deleteBtn = div.querySelector(".fa-xmark");

            deleteBtn.addEventListener("click", function () {
                itemsAñadidos.splice(index, 1);
                renderCarrito();
            });

            carrito.appendChild(div);
        });
    }
}


function mostrarConfirmacion(titulo, mensaje, onConfirm) {
    confirmCallback = onConfirm;

    document.getElementById("modalTitle").innerText = titulo;
    document.getElementById("modalMessage").innerText = mensaje;
    document.getElementById("confirmModal").classList.remove("hidden");

}

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

                if (!filaAEliminar) return;

                const costeEliminado = parseFloat(filaAEliminar.dataset.coste);
                const totalElement = document.getElementById("totalCoste");
                const totalActual = parseFloat(totalElement.innerText);

                const nuevoTotal = totalActual - costeEliminado;
                totalElement.innerText = nuevoTotal.toFixed(2);

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
        case 'inv':
            $.ajax({
                url: '/Inventario/_' + http + 'InventarioView',
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




