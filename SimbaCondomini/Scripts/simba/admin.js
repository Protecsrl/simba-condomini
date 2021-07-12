
var markerUrl = "https://js.devexpress.com/Demos/RealtorApp/images/map-marker.png";


function onValueRegioneChanged(e) {
    let comboProvincie = $("#comboProvince").dxSelectBox("instance");
    let dataSource = comboProvincie.getDataSource();
    dataSource.filter("Id", "=", e.value);
    dataSource.load();
    comboProvincie.option("value", null);
}


function onValueProvinciaChanged(e) {
    let comboComune = $("#comboComuni").dxSelectBox("instance");
    let dataSource = comboComune.getDataSource();
    dataSource.filter("Id", "=", e.value);
    dataSource.load();
    comboComune.option("value", 0);
}


function GoToNewCondominio(e) {
    var buttonText = e.component.option("text");
    // DevExpress.ui.notify("The " + buttonText + " button was clicked");
    window.location.href = "AdminCondomini/Edit";
}

function CondominioMapClicked(e) {
    console.log(e);
}