function showDiv()
{
    const element = document.getElementById('type')
    if (element.value === "pull_request") {
        document.getElementById('issueOptions').style.display = 'none';
        document.getElementById('prOptions').style.display = 'block';
    } else {
        document.getElementById('issueOptions').style.display = 'block';
        document.getElementById('prOptions').style.display = 'none';
    }
}