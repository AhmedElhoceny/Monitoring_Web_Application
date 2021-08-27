let GlobalURL = "https://localhost:44325/";

document.querySelector(".OurMembers").children[0].style.display = "none";
let ActivateMember = (MemberID) => {
    fetch(GlobalURL + 'Home/ActivateMember?id=' + MemberID)
        .then(response => console.log(response));
}
