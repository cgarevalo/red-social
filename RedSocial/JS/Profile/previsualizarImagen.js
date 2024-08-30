// Función para previsualizar la imagen seleccionada en el control FileUpload antes de cargarla en el servidor
function previewImage() {

    // Obtiene el control FileUpload y el control Image por sus IDs de las variables creadas en el bloque de script en el Content1 de Profile.aspx
    var fileUpload = document.getElementById(fuProfileImageClientId);
    var imgPerfil = document.getElementById(imgImagePreviewClientId);

    // Verifica si se ha seleccionado un archivo
    if (fileUpload.files && fileUpload.files[0]) {

        // Crea un nuevo FileReader para leer el contenido del archivo
        var reader = new FileReader();

        // Define la función que se ejecuta cuando el archivo se ha leído completamente
        reader.onload = function (e) {

            // Asigna el contenido leído (URL de la imagen) al control Image para previsualización
            imgPerfil.src = e.target.result;
        };

        // Lee el contenido del archivo como una URL de datos
        reader.readAsDataURL(fileUpload.files[0]);
    }
}