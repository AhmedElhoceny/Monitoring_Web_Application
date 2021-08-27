let GlobalURL = "https://localhost:44325/";

let flag = 0;
let CheckSelection = (x) => {
    document.querySelectorAll(".JoinedTeam .Teams div img").forEach(element => {
        element.classList.remove("Selected");
        flag = 1;
    });
    x.children[0].classList.toggle("Selected");
}
let AddTeam = (MemberID) => {
    if (flag == 1) {
        let SelectedTeam = document.querySelector(".Selected").parentElement.children[1].textContent;
        fetch(GlobalURL + 'Home/AddTeamPost?id=' + MemberID + "&TeamName=" + SelectedTeam)
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
    } else {
        alert("Please Choose Team First :)");
    }
}
let hideResults = () => {
    document.querySelector(".Done").style.top = "-100%";
    document.querySelector(".Failed").style.top = "-100%";
}