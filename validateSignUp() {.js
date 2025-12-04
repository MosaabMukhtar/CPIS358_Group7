function validateSignUp() {
    var fname = document.getElementById("fname").value;
    var lname = document.getElementById("lname").value;
    var email = document.getElementById("email").value;
    var username = document.getElementById("uname").value;
    var password = document.getElementById("pword").value;

    if (fname === "") {
        alert("First Name cannot be empty or whitespace only.");
        return false;
    }

    if (lname === "") {
        alert("Last Name cannot be empty or whitespace only.");
        return false;
    }

    if (email === "") {
        alert("Email cannot be empty.");
        return false;
    }

    if (username === "") {
        alert("Username cannot be empty or whitespace only.");
        return false;
    }

    if (password === "") {
        alert("Password cannot be empty.");
        return false;
    }

    if (password.length < 8) {
        alert("Password must be at least 8 characters long.");
        return false;
    }

    return true;
}