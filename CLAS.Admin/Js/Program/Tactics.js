(function () {
    function Tractics() {
        this.init();
    }
    Tractics.prototype = {
        init: function () {
            var self = this;
            self.bindUi();
        },
        bindUi: function () {

            var self = this, id = $("#TacticsId").val();
            $.ajax({
                url: "/Tactics/GetTactics/" + id,
                success: function (a) {
                    window.model = new Vue({
                        el: '#form1',
                        data: a,
                        methods: self.formEvent
                    });
                }
            });


            $("#ScriptSelect").change(function () {
                var html = $("option:selected", this).html();
                model.tempScript.Script["ScriptName"] = html;
            });
        },
        formEvent: {
            editExecute: function (index,a) {
              Global.CopyOb(a, model.tempScript);
                model.tempScript.index = index;
            },
            editToExecute: function () {
                var a = Global.CopyOb(model.tempScript);
                model.Scripts.$set(model.tempScript.index,a);

                Global.EmptyObject(model.tempScript);
            },
            deleteExecute: function (a) {
                model.Scripts.$remove(a);

            },
            cancelToExecute:function() {
                Global.EmptyObject(model.tempScript);
            },
            addToExecute: function () {
                var a = Global.CopyOb(model.tempScript);
                model.Scripts.push(a);
                Global.EmptyObject(model.tempScript);
            },
            save: function () {
                var a = {};
                a.Id = model.Id;
                a.TacticsName = model.TacticsName;
                a.Description = model.Description;
                a.PriceScript = model.PriceScript;
                a.SyncStopTimeBegin = model.SyncStopTimeBeginName;
                a.SyncStopTimeStop = model.SyncStopTimeStopName;
                a.Scripts = model.Scripts;
                $.ajax({
                    dataType: "json",
                    url: "/Tactics/SaveTactics",
                    data: JSON.stringify(a),
                    type: "post",
                    contentType: "application/json",
                    success: function(a) {
                        if (a.code == 1) {
                            Global.Utils.ShowMessage("保存成功！");
                        }
                    }
                });
            }
        }

    }
    return new Tractics();
})()