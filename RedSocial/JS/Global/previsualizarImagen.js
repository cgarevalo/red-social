// Función para previsualizar la imagen seleccionada en el control FileUpload antes de cargarla en el servidor
function previewImage(fileUploadId, imgPreviewId) {

    // Obtiene el control FileUpload y el control Image por sus IDs de las variables creadas en el bloque de script en el Content1 de Profile.aspx
    let fileUpload = document.getElementById(fileUploadId);
    let imgPerfil = document.getElementById(imgPreviewId);
    let btnRemoveImage = document.getElementById(btnRemoveImageClientId);

    // Verifica si se ha seleccionado un archivo
    if (fileUpload.files && fileUpload.files[0]) {

        // Crea un nuevo FileReader para leer el contenido del archivo
        var reader = new FileReader();

        // Define la función que se ejecuta cuando el archivo se ha leído completamente
        reader.onload = function (e) {

            // Asigna el contenido leído (URL de la imagen) al control Image para previsualización
            imgPerfil.src = e.target.result;

            btnRemoveImage.style.display = "block"; // Mostrar el botón de cierre "X"
        };

        // Lee el contenido del archivo como una URL de datos
        reader.readAsDataURL(fileUpload.files[0]);
    } else {
        // Si no hay archivo seleccionado, limpia la previsualización
        imgPerfil.src = "";
        btnRemoveImage.style.display = "none"; // Ocultar el botón de cierre "X"
    }
}