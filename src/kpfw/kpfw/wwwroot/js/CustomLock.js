// Custom form lock
function CustomLock(submitControl, hiddenField, customText) {
    customText = typeof customText !== 'undefined' ? customText : "Check this box to prove you're human";
    var sbmt = document.getElementById(submitControl);
    sbmt.setAttribute('disabled', 'disabled');
    sbmt.insertAdjacentHTML('beforebegin', "<p class='custom-lock'><label><input type='checkbox' name='cutomLock' onclick='UnlockForm(this, \"" + submitControl + "\", \"" + hiddenField + "\")' /> " + customText + "</label></p>");
    sbmt.insertAdjacentHTML('beforebegin', "<input type='hidden' value='' name='" + hiddenField + "' id='" + hiddenField + "' />");
    hField = hiddenField;
    sControl = submitControl;
}

function UnlockForm(checkControl, sControl, hField) {
    var sbmt = document.getElementById(sControl);
    var hf = document.getElementById(hField);
    if (checkControl.checked) {
        sbmt.removeAttribute("disabled");
        hf.value = "10";
    } else {
        sbmt.setAttribute('disabled', 'disabled');
        hf.value = "";
    }
}