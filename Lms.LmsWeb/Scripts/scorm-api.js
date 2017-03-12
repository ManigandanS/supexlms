
var consoleAvailable = false;

if (window.console && window.console.log) {
    consoleAvailable = true;
}

//SCORM 2004 API
function API_13() {

    //access constants
    var A_READ = 0;
    var A_WRITE = 1;
    var A_READWRITE = 2;

    //running states
    var S_NOTINITIALIZED = 0;
    var S_RUNNING = 1;
    var S_FINISHED = 2;

    var api_state = S_NOTINITIALIZED;
    var diagnostic = "";
    var error_code = "0";
    var dt_count = 0;

    //initialize
    function Initialize(param) {
        error_code = "0";
        if (param == "") {
            if (api_state == S_NOTINITIALIZED) {
                api_state = S_RUNNING;
                error_code = "0";

                if (consoleAvailable) {
                    console.log("LMSInitialize");
                }

                return true;
            } else {
                error_code = "101"; 	//already initialized
            }
        } else {
            error_code = "201";
        }

        if (consoleAvailable) {
            console.log("Initialize error: " + GetErrorString(error_code));
        }

        return false;
    }

    //terminate
    function Terminate(param) {
        error_code = "0";
        if (param == "") {
            api_state = S_FINISHED;

            if (consoleAvailable) {
                console.log("Terminate");
            }

            return true;
        } else {
            error_code = "201";
        }

        if (consoleAvailable) {
            console.log("Terminate error: " + GetErrorString(error_code));
        }

        parent.window.location.href = $('#redirectUrl').val();

        return false;
    }

    /*
    * Data-Transfer Methods
    */

    //request information from LMS
    function GetValue(param) {
        error_code = "0";
        var enid = $("#enid").val();
        var lsid = $("#lsid").val();
        var value = "";
        var obj = new Object();
        obj.enid = enid;
        obj.lsid = lsid;
        obj.param = param;


        if (consoleAvailable) {
            console.log("GetValue enid: " + enid + ", param: " + param + ", ap_state: " + api_state);
        }
        if (api_state == S_RUNNING) {
            // GET data
            $.ajax({
                type: "POST",
                url: "ScormApi.svc/GetValue",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    if (consoleAvailable) {
                        console.log("GetValue Success! --> " + response.d);
                    }
                    if (response != null) {
                        value = response.d;
                    }
                },
                failure: function (response) {
                    console.log("LMSGetValue fail! ");
                    console.log(response.d);
                },
                error: function (request, status, error) {
                    if (consoleAvailable) {
                        console.log("LMSGetValue error! ");
                        console.log(error);
                    }
                }
            });
        } else {
            error_code = "301";
        }

        if (error_code != "0") {
            if (consoleAvailable) {
                console.log("GetValue(" + param + ") returned error: " + GetErrorString(error_code));
            }
        }

        return value;
    }

    function SetValue(param, value) {
        var retValue = "false";
        error_code = "0";
        var enid = $("#enid").val();
        var lsid = $("#lsid").val();

        var obj = new Object();
        obj.enid = enid;
        obj.lsid = lsid;
        obj.param = param;
        obj.value = value;

        if (consoleAvailable) {
            console.log("SetValue enid: " + enid + ", param: " + param + ", value: " + value);
        }
        if (api_state == S_RUNNING) {
            //save data to LMS
            $.ajax({
                type: "POST",
                url: "ScormApi.svc/SetValue",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    retValue = "true";
                },
                failure: function (response) {
                    alert(response.d);
                }
            });


        } else {
            error_code = "301"; 	//not initialized
        }

        if (error_code != "0") {
            if (consoleAvailable) {
                console.log("SetValue(" + param + "," + value + ") returned error: " + GetErrorString(error_code));
            }
        }

        return retValue;
    }

    //commit all values not persisted
    function Commit(param) {
        error_code = "0";

        if (api_state == S_RUNNING) {

            var obj = new Object();
            obj.enid = $("#enid").val();
            obj.lsid = $("#lsid").val();

            // COMMIT data
            $.ajax({
                type: "POST",
                url: "ScormApi.svc/Commit",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    if (consoleAvailable) {
                        console.log("Commit Success! --> " + response.d);
                    }
                    if (response != null) {
                        value = response.d;
                    }
                },
                failure: function (response) {
                    console.log("Commit fail! ");
                    console.log(response.d);
                },
                error: function (request, status, error) {
                    if (consoleAvailable) {
                        console.log("Commit error! ");
                        console.log(error);
                    }
                }
            });

            return true;
        } else {
            error_code = "301";
        }

        return false;
    }

    function GetLastError() {
        return error_code;
    }

    function GetErrorString(param) {
        if (param != "") {
            var error_string = "";
            switch (param) {
                case "0":
                    error_string = "No error";
                    break;
                case "101":
                    error_string = "General exception";
                    break;
                case "201":
                    error_string = "Invalid argument error";
                    break;
                case "202":
                    error_string = "Element cannot have children";
                    break
                case "203":
                    error_string = "Element not an array - cannot have count";
                    break;
                case "301":
                    error_string = "Not initialized";
                    break;
                case "401":
                    error_string = "Not implemented error";
                    break;
                case "402":
                    error_string = "Invalid set value, element is a keyword";
                    break;
                case "403":
                    error_string = "Element is read only";
                    break;
                case "404":
                    error_string = "Element is write only";
                    break;
                case "405":
                    error_string = "Incorrect data type";
                    break;
            }
            return error_string;
        } else {
            return "";
        }
    }

    function GetDiagnostic(param) {
        if (param == "") {
            param = error_code;
        }
        return param;
    }

    this.Initialize = Initialize;
    this.Terminate = Terminate;
    this.GetValue = GetValue;
    this.SetValue = SetValue;
    this.Commit = Commit;
    this.GetLastError = GetLastError;
    this.GetErrorString = GetErrorString;
    this.GetDiagnostic = GetDiagnostic;

    //end of API functions
    //-------------------------------------------------------------------



    //create 6 random characters
    function randomString() {
        var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
        var string_length = 6;
        var randomstring = '';
        for (var i = 0; i < string_length; i++) {
            var rnum = Math.floor(Math.random() * chars.length);
            randomstring += chars.substring(rnum, rnum + 1);
        }
        return randomstring;
    }


    function isInteger(n) {
        var ValidChars = "0123456789";
        var IsNumber = true;
        var Char;
        for (i = 0; i < n.length && IsNumber == true; i++) {
            Char = n.charAt(i);
            if (ValidChars.indexOf(Char) == -1)
                IsNumber = false;
        }
        return IsNumber;
    }

}

var API_1484_11 = new API_13();


//SCORM 1.2 API
function API_12() {

    //access constants
    var A_READ = 0;
    var A_WRITE = 1;
    var A_READWRITE = 2;

    //running states
    var S_NOTINITIALIZED = 0;
    var S_RUNNING = 1;
    var S_FINISHED = 2;

    var api_state = S_NOTINITIALIZED;
    var diagnostic = "";
    var error_code = "0";
    var dt_count = 0;

    //initialize
    function LMSInitialize(param) {
        error_code = "0";

        if (param == "") {
            if (api_state == S_NOTINITIALIZED) {
                api_state = S_RUNNING;
                error_code = "0";

                if (consoleAvailable) {
                    console.log("LMSInitialize");
                }

                return true;
            } else {
                error_code = "101";		//already initialized
            }
        } else {
            error_code = "201";
        }

        if (consoleAvailable) {
            console.log("LMSInitialize error: " + LMSGetErrorString(error_code));
        }


        return false;

    }

    //finish
    function LMSFinish(param) {
        error_code = "0";
        if (param == "") {
            api_state = S_FINISHED;

            if (consoleAvailable) {
                console.log("LMSFinish");
            }

            return true;
        } else {
            error_code = "201";
        }

        if (consoleAvailable) {
            console.log("LMSFinish error: " + LMSGetErrorString(error_code));
        }


        return false;
    }

    /*
	 * Data-Transfer Methods
	 */

    //request information from LMS
    function LMSGetValue(param) {
        error_code = "0";
        var enid = $("#enid").val();
        var lsid = $("#lsid").val();
        var value = "";
        var obj = new Object();
        obj.enid = enid;
        obj.lsid = lsid;
        obj.param = param;


        if (consoleAvailable) {
            console.log("LMSGetValue enid: " + enid + ", param: " + param + ", ap_state: " + api_state);
            console.log(JSON.stringify(obj));
        }
        if (api_state == S_RUNNING) {
            // GET data

            $.ajax({
                type: "POST",
                url: "ScormApi.svc/LmsGetValue",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    if (consoleAvailable) {
                        console.log("GetValue Success! --> " + response.d);
                    }

                    if (response != null) {
                        value = response.d;
                    }
                },
                failure: function (response) {
                    if (consoleAvailable) {
                        console.log("LMSGetValue fail! ");
                        console.log(response.d);
                    }
                },
                error: function (request, status, error) {
                    if (consoleAvailable) {
                        console.log("LMSGetValue error! ");
                        console.log(error);
                    }
                }
            });

        } else {
            error_code = "301";
        }

        if (error_code != "0") {
            if (consoleAvailable) {
                console.log("LMSGetValue(" + param + ") returned error: " + LMSGetErrorString(error_code));
            }
        }

        return value;
    }

    function LMSSetValue(param, value) {
        var retValue = "false";
        error_code = "0";
        var enid = $("#enid").val();
        var lsid = $("#lsid").val();
        var obj = new Object();
        obj.enid = enid;
        obj.lsid = lsid;
        obj.param = param;
        obj.value = value;

        if (consoleAvailable) {
            console.log("LMSSetValue enid: " + enid + ", param: " + param + ", value: " + value);
            console.log(JSON.stringify(obj));
        }

        if (api_state == S_RUNNING) {
            //save data to LMS
            $.ajax({
                type: "POST",
                url: "ScormApi.svc/LmsSetValue",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    if (consoleAvailable) {
                        console.log("LMSSetValue success! ");
                    }
                    retValue = "true";
                },
                failure: function (response) {
                    if (consoleAvailable) {
                        console.log("LMSSetValue failed...");
                    }
                }
            });


        } else {
            error_code = "301";		//not initialized
        }

        if (error_code != "0") {
            if (consoleAvailable) {
                console.log("LMSSetValue(" + param + "," + value + ") returned error: " + LMSGetErrorString(error_code));
            }
        }

        return retValue;

    }

    //commit all values not persisted
    function LMSCommit(param) {
        error_code = "0";

        if (consoleAvailable) {
            console.log("LMSCommit(" + param + ")");
        }


        if (api_state == S_RUNNING) {

            var obj = new Object();
            obj.enid = $("#enid").val();
            obj.lsid = $("#lsid").val();

            // COMMIT data
            $.ajax({
                type: "POST",
                url: "ScormApi.svc/LmsCommit",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    if (consoleAvailable) {
                        console.log("Commit Success! --> " + response.d);
                    }
                    if (response != null) {
                        value = response.d;
                    }
                },
                failure: function (response) {
                    console.log("Commit fail! ");
                    console.log(response.d);
                },
                error: function (request, status, error) {
                    if (consoleAvailable) {
                        console.log("Commit error! ");
                        console.log(error);
                    }
                }
            });

            return true;
        } else {
            error_code = "301";
        }

        return false;
    }

    function LMSGetLastError() {
        return error_code;
    }

    function LMSGetErrorString(param) {
        if (param != "") {
            var error_string = "";
            switch (param) {
                case "0":
                    error_string = "No error";
                    break;
                case "101":
                    error_string = "General exception";
                    break;
                case "201":
                    error_string = "Invalid argument error";
                    break;
                case "202":
                    error_string = "Element cannot have children";
                    break
                case "203":
                    error_string = "Element not an array - cannot have count";
                    break;
                case "301":
                    error_string = "Not initialized";
                    break;
                case "401":
                    error_string = "Not implemented error";
                    break;
                case "402":
                    error_string = "Invalid set value, element is a keyword";
                    break;
                case "403":
                    error_string = "Element is read only";
                    break;
                case "404":
                    error_string = "Element is write only";
                    break;
                case "405":
                    error_string = "Incorrect data type";
                    break;
            }
            return error_string;
        } else {
            return "";
        }
    }

    function LMSGetDiagnostic(param) {
        if (param == "") {
            param = error_code;
        }
        return param;
    }

    this.LMSInitialize = LMSInitialize;
    this.LMSFinish = LMSFinish;
    this.LMSGetValue = LMSGetValue;
    this.LMSSetValue = LMSSetValue;
    this.LMSCommit = LMSCommit;
    this.LMSGetLastError = LMSGetLastError;
    this.LMSGetErrorString = LMSGetErrorString;
    this.LMSGetDiagnostic = LMSGetDiagnostic;

    //end of API functions
    //-------------------------------------------------------------------



    //create 6 random characters
    function randomString() {
        var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
        var string_length = 6;
        var randomstring = '';
        for (var i = 0; i < string_length; i++) {
            var rnum = Math.floor(Math.random() * chars.length);
            randomstring += chars.substring(rnum, rnum + 1);
        }
        return randomstring;
    }


    function isInteger(n) {
        var ValidChars = "0123456789";
        var IsNumber = true;
        var Char;
        for (i = 0; i < n.length && IsNumber == true; i++) {
            Char = n.charAt(i);
            if (ValidChars.indexOf(Char) == -1)
                IsNumber = false;
        }
        return IsNumber;
    }

}

var API = new API_12();

function OnSuccess(response) {
    if (consoleAvailable) {
        console.log("response success!");
    }
}