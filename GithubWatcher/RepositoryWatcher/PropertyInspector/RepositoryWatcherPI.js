function showDiv(divId, element)
{
    if (element.value === "pull-requests") {
        document.getElementById('issueOptions').style.display = 'none';
        document.getElementById('prOptions').style.display = 'block';
    } else {
        document.getElementById('issueOptions').style.display = 'block';
        document.getElementById('prOptions').style.display = 'none';
    }
}