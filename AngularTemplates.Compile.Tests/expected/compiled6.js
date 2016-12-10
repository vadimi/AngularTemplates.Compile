angular.module('app', []).run(['$templateCache', function ($templateCache) {
$templateCache.put('template3.html', "\u003cdiv\u003etest\u003c/div\u003e");
}]);