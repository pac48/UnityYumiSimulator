
(cl:in-package :asdf)

(defsystem "dynamics-srv"
  :depends-on (:roslisp-msg-protocol :roslisp-utils )
  :components ((:file "_package")
    (:file "getStaticTorques" :depends-on ("_package_getStaticTorques"))
    (:file "_package_getStaticTorques" :depends-on ("_package"))
    (:file "setTorques" :depends-on ("_package_setTorques"))
    (:file "_package_setTorques" :depends-on ("_package"))
  ))