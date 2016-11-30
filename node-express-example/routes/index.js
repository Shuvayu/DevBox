var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function(req, res, next) {
  //res.render('index', { title: 'Express' });
  GetData(function(recordset){
    res.render('index',{products: recordset});
  });
});

function GetData(callback) {
  var sql = require('mssql');
  var config = {
    user: 'sa',
    password: '4mation!',
    database: '', // dB NAME
    server: 'localhost'
  };
  var connection = new sql.Connection(config, function(err){
    // check for errors by inspecting the error parameter
    var request = new sql.request(connection);
    request.quesry('select * from products', function(err,recordset){
      callback(recordset);
    });
  });
}

module.exports = router;
