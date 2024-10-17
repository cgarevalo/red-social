function checkPostType() {
    alert("JavaScript function called!"); // Verifica si la función es llamada
    //const ddlTypePost = document.getElementById(ddlTypePostClientId);
    var fuFotos = document.getElementById(fuFotosPostClientId);
    var fuVideos = document.getElementById(fuVideosPostClientId);

    if (fuFotos.value !== "" || fuVideos.value !== "") {
        // Si hay archivos seleccionados, forzar un postback completo
        console.log("Archivo seleccionado, se requiere postback completo");

        __doPostBack('<%= btnCreatePost.UniqueID %>', '');
        return false; // Previene el postback asincrónico
    }

    // Verifica si el valor seleccionado corresponde a "foto" o "video"
    //if (ddlTypePost.value === "Video" || ddlTypePost.value === "Foto") {
    //    // Si hay archivos seleccionados, forzar un postback completo
    //    __doPostBack('<%= btnCreatePost.UniqueID %>', '');
    //    return false; // Previene el postback asincrónico
    //}

    console.log("No hay archivos seleccionados, continúa con el postback parcial");
    return true; // Permite el postback asincrónico si no hay archivos
}