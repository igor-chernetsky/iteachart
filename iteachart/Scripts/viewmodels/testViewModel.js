$(function(){
    function TestViewModel(data) {
        var self = this;
            
        self.attemptId = data.attemptId;
        self.startCount = data.jsonQuestions.length;
        self.questionIndex = ko.observable(0);
        self.questions = ko.observableArray(data.jsonQuestions);
        self.score = ko.observable(0);
        self.status = ko.observable(null);
        self.inProcess = ko.observable(false);
        self.finished = ko.observable(false);
        self.results = ko.observable(null);
        
        self.wrongAnswers = ko.observableArray([]);

        self.incrementQuestionIndex = function () {
            var newValue = self.questionIndex() + 1;
            if (newValue >= self.questions().length) newValue = 0;
            self.questionIndex(newValue);
        }

        self.getResult = function () {

        }

        self.sendAnswer = function (item) {
            self.inProcess(true);
            var data = { id: item.id, answer: item.answer, attemptId: self.attemptId };
            $.ajax({
                url: '/Quiz/check',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                data: data,
                success: function (response) {
                    response = $.parseJSON(response);
                    console.log(response);
                    if (response.status) {
                        self.status('correct');
                    } else {
                        self.status('wrong');
                        self.wrongAnswers.push({
                            text: item.text,
                            answer: response.answer
                        });
                    }
                    setTimeout(function () {
                        self.inProcess(false);
                        self.status(null);
                        self.questions.remove(item);
                        self.incrementQuestionIndex();

                        if (!self.questions().length) {
                            setResults();
                        }
                    }, 2000);
                }
            });
        }

        self.GetResultString = function (results) {
            var percent = 0;
            if(self.startCount){
                percent = (100/self.startCount)*results();
            }
            var resstring = 'You answered on ' + percent + '% you ' + percent >= 80 ? 'pass' : 'failed';
            return resstring;
        }

        function setResults() {
            var data = { attemptId: self.attemptId };

            $.ajax({
                url: '/Quiz/GetResults',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                data: data,
                success: function (response) {
                    response = $.parseJSON(response);
                    self.results(response.score);
                    self.finished(true);
                }
            });
        }
    }
    var data = getJsonQuestions();
    ko.applyBindings(new TestViewModel(data), document.getElementById('testContainer'))
})