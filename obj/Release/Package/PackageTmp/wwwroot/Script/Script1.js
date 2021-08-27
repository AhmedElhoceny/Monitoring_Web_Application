let GlobalURL2 = "https://localhost:44325/";

let ShowMenuChoose = () => {
    document.querySelector(".ChooseMenu").style.display = "flex ";
}
let HideMeniChoose = () => {
    document.querySelector(".ChooseMenu").style.display = "none ";
}
let DoneTask = (MemberID, TaskID) => {
    console.log("Hello");
    fetch(GlobalURL2 + 'Home/DoneTask?MemberID=' + MemberID + "&TaskID=" + TaskID )
        .then(response => response.json())
        .then(data => {
            console.log(data.Result);
            if (data.Result == "OK") {
                window.scroll({
                    top: 0,
                    left: 0,
                    behavior: 'smooth'
                });
                document.querySelector(".Done").style.top = "100px";
                document.querySelector(".Failed").style.top = "-100%";
            } else {
                window.scroll({
                    top: 0,
                    left: 0,
                    behavior: 'smooth'
                });
                document.querySelector(".Done").style.top = "-100%";
                document.querySelector(".Failed").style.top = "100px";
            }
        });
}

let FailTask = (MemberID, TaskID) => {
    fetch(GlobalURL2 + 'Home/FailTask?MemberID=' + MemberID + "&TaskID=" + TaskID)
        .then(response => response.json())
        .then(data => {
            console.log(data.Result);
            if (data.Result == "OK") {
                window.scroll({
                    top: 0,
                    left: 0,
                    behavior: 'smooth'
                });
                document.querySelector(".Done").style.top = "100px";
                document.querySelector(".Failed").style.top = "-100%";
            } else {
                window.scroll({
                    top: 0,
                    left: 0,
                    behavior: 'smooth'
                });
                document.querySelector(".Done").style.top = "-100%";
                document.querySelector(".Failed").style.top = "100px";
            }
        });
}

let hideResults2 = () => {
    document.querySelector(".Done").style.top = "-100%";
    document.querySelector(".Failed").style.top = "-100%";
}