

function search() {
    var area = document.getElementById("dia-diem").value;
    var job = document.getElementById("linh-vuc").value;
    var name = document.getElementById("key-word").value;
    console.log(name);
    var dbParam = "Name=" + name + "&AreaID=" + area + "&JobID=" + job;
    window.location.href = "/SearchCandidateResult?" + dbParam;
}

function refresh() {
    if (document.getElementById("linh-vuc").value != 0 || document.getElementById("dia-diem").value != 0 || document.getElementById("key-word").value) {
        console.log(document.getElementById("key-word").value);
        window.location.href = "/SearchCandidateResult";
        document.getElementById("linh-vuc").value = 0;
        document.getElementById("dia-diem").value = 0;
        document.getElementById("key-word").value = "";

    }
}

function checkName() {
    var name = document.getElementById("key-word").value;
    var name1 = "";
    var i = 1;
    name1 = name.charAt(0).toUpperCase();
    while (i < name.length) {
        if (name.charAt(i) == " " && name.charAt(i + 1) == " ") {
            i++;
            if (i == name.length) break;
        }
        else if (name.charAt(i - 1) == " ") {
            name1 = name1 + name.charAt(i).toUpperCase();
            i++;
        }
        else {
            name1 = name1 + name.charAt(i);
            i++;
        }
    }
    document.getElementById("key-word").value = name1;
}