angular.module('myapp', []).run(['$templateCache', function ($templateCache) {
$templateCache.put('/templates/template1.html', "test");
$templateCache.put('/templates/template2.html', "test2");
}]);