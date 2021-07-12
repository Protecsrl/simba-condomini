function onValueRegioneChanged(e) {
    let comboRegioni = $("#comboRegioni").dxSelectBox("instance");
    let dataSource = comboRegioni.getDataSource();
    dataSource.filter("Id", "=", e.value);
    dataSource.load();
    comboRegioni.option("value", null);
}


function onValueProvinciaChanged(e) {
    let comboProvincia = $("#comboProvincie").dxSelectBox("instance");
    let dataSource = comboProvincia.getDataSource();
    dataSource.filter("Id", "=", e.value);
    dataSource.load();
    comboProvincia.option("value", 0);
}


function GoToNewCondominio(e) {
    var buttonText = e.component.option("text");
    // DevExpress.ui.notify("The " + buttonText + " button was clicked");
    window.location.href = "AdminCondomini/Edit";
}