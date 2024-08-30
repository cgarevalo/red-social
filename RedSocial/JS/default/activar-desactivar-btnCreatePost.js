function checkInput() {
    // Obtine los elementos por su ClientID generado por ASP.NET
    let textBox = document.getElementById(txtPostClientId);
    let postButton = document.getElementById(btnPostClientId);

    // Habilita o deshabilita el botón según el contenido del TextBox
    postButton.disabled = textBox.value.trim() === "";
}